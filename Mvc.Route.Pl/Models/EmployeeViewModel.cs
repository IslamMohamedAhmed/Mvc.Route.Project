using Mvc.Route.Dal.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Route.Pl.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }
        [Range(25, 60, ErrorMessage = "Age limit should be between 25 & 60")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Address is required!")]
        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,20}-[a-zA-Z]{5,20}-[a-zA-Z]{5,20}$", ErrorMessage = "Address should be in the form 123-street-city-country")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Salary is required!")]
        public decimal Salary { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email Address is required!")]
        public string Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "Phone Number is required!")]
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;
        [Required(ErrorMessage = "Hiring date is required!")]
        public DateTime HiringDate { get; set; }


        [ForeignKey("WorkFor")]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department? WorkFor { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
    }
}
