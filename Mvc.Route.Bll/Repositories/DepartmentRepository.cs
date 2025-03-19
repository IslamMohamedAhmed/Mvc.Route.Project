using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc.Route.Bll.Interfaces;
using Mvc.Route.Dal.Data.Contexts;
using Mvc.Route.Dal.Models;

namespace Mvc.Route.Bll.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        

        public DepartmentRepository(AppDbContext appDbContext) : base(appDbContext) { }
        
        
    }
}
