using System.ComponentModel.DataAnnotations;

namespace Mvc.Route.Pl.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "First Name is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required!")]
        public string LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Confirmation is required!")]
        [Compare(nameof(Password),ErrorMessage ="Passwords don't match!")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "you need to agree to wesite policy!")]
        public bool IsAgree { get; set; }
    }
}
