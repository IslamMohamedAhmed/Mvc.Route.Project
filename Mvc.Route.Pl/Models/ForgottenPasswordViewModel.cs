using System.ComponentModel.DataAnnotations;

namespace Mvc.Route.Pl.Models
{
    public class ForgottenPasswordViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
       
    }
}
