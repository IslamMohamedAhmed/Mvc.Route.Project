using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Route.Dal.Models
{
    public class Department : BaseEntityInt
    {
      
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        
        public DateTime DateOfCreation { get; set; }
        [InverseProperty("WorkFor")]
        
        
        public ICollection<Employee>? Employees { get; set; }
    }
}
