﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mvc.Route.Dal.Models
{
    public class Employee : BaseEntityInt
    {
        
        public string Name { get; set; }
        
        public int? Age { get; set; }
       
        public string Address { get; set; }
        
        public decimal Salary { get; set; }
      
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = false;
       
        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        [ForeignKey("WorkFor")]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        public Department? WorkFor { get; set; }

        public string? ImageName { get; set; }
    }
}
