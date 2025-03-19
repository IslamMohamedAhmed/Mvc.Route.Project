using Mvc.Route.Dal.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Route.Pl.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "code is required!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "date of creation  is required!")]
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("WorkFor")]
        public ICollection<Employee>? Employees { get; set; }
    }
}
