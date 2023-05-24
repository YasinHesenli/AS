using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

     

        public async Task< IActionResult> Index()
        {
           List<Team> team = await _context.teams.Include(x=>x.Position).ToListAsync();
            return View(team);
        }

        public async Task< IActionResult> Create()
        {
            ViewBag.Position = await _context.positions.ToListAsync();    
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Team team)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View();
            //}


            if (!team.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Photo ola biler ancaq");
                return View();
            }
            if (team.Photo.Length > 200 * 1024)
            {
                ModelState.AddModelError("Photo", "Photo 200 den boyuk olanmaz");
                return View();
            }

            string filename = Guid.NewGuid().ToString() + team.Photo.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", filename);

            using (FileStream file = new FileStream(path,FileMode.Create))
            {
                await team.Photo.CopyToAsync(file);
            }
            team.Image = filename;

            await _context.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            Team existed = await _context.teams.FirstOrDefaultAsync(x=>x.Id== id);
            if (existed==null)
            {
                return View();
            }
            string path = Path.Combine(_env.WebRootPath, "assets/img", existed.Image);
            System.IO.File.Delete(path);


            _context.teams.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }
        public async Task< IActionResult> Update()
        {
            ViewBag.Position = await _context.positions.ToListAsync();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Update(Team team)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            Team existed = _context.teams.FirstOrDefault(x=>x.Id== team.Id);
            if(existed!=null)
            {
                if (!team.Photo.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photo", "Photo ola biler ancaq");
                    return View();
                }
                if (team.Photo.Length > 200 * 1024)
                {
                    ModelState.AddModelError("Photo", "Photo 200 den boyuk olanmaz");
                    return View();
                }

                string path = Path.Combine(_env.WebRootPath,"assets/img",existed.Image);

                System.IO.File.Delete(path);
                string newfilename  = Guid.NewGuid().ToString()+team.Photo.FileName;

                string newpath = Path.Combine(_env.WebRootPath, "assets/img", newfilename);
                using (FileStream file = new FileStream(newpath,FileMode.Create))
                {
                    await team.Photo.CopyToAsync(file);
                }

                existed.Image = newfilename;

            }

            existed.Name=team.Name;
            existed.Title=team.Title;
            existed.Position = team.Position;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));



        }

    }
}
