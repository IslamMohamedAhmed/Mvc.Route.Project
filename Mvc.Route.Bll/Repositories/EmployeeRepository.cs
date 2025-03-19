using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mvc.Route.Bll.Interfaces;
using Mvc.Route.Dal.Data.Contexts;
using Mvc.Route.Dal.Models;

namespace Mvc.Route.Bll.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext):base(appDbContext) { }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
            return await appDbContext.Employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(i=>i.WorkFor).ToListAsync();
        }
    }
}
