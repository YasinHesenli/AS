using System.ComponentModel.DataAnnotations;

namespace Lumia.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string UsernameorEmail { get; set; }

        [Required]
        [DataType(DataType.Password) ]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
