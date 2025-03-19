using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mvc.Route.Dal.Models;

namespace Mvc.Route.Dal.Data.Configurations
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p=>p.Id).UseIdentityColumn(8,8);
            builder.Property(p => p.Salary).HasColumnType("decimal(18,3)");
        }
    }
}
