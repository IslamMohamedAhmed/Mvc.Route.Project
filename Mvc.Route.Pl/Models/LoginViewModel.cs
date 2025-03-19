using System.ComponentModel.DataAnnotations;

namespace Mvc.Route.Pl.Models
{
    public class LoginViewModel
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
