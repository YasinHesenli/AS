using Lumia.Models;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Controllers
{
    public class AccauntController : Controller
    {
        readonly UserManager<AppUser> _usermanager;
        readonly SignInManager<AppUser> _signInManager;

        public AccauntController(UserManager<AppUser> usermanager, SignInManager<AppUser> signInManager)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
        }

       
        


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM user)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}
			AppUser exis = new AppUser
			{
				Name = user.Name,
				Email = user.Email,
				Surname = user.Surname,
				UserName = user.UserName


			};
			IdentityResult result = await _usermanager.CreateAsync(exis, user.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					ModelState.AddModelError(string.Empty, item.Description);
					return View();

				}
			}

			await _signInManager.SignInAsync(exis, false);

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]

		public async Task<IActionResult> Login(LoginVm user)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			AppUser existed = await _usermanager.FindByEmailAsync(user.UsernameorEmail);
			if (existed == null)
			{
				existed = await _usermanager.FindByNameAsync(user.UsernameorEmail);
				if (existed == null)
				{
					ModelState.AddModelError(String.Empty,"Bele username var ");
					return View();
				}
			}

			await _signInManager.PasswordSignInAsync(existed, user.Password, user.RememberMe, false);

			return RedirectToAction("Index","Home");
		}

	}
}
