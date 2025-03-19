using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc.Route.Bll.Interfaces;
using Mvc.Route.Dal.Data.Contexts;

namespace Mvc.Route.Bll.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;
        private IDepartmentRepository _DepartmentRepository;
        private IEmployeeRepository _EmployeeRepository;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _DepartmentRepository = new DepartmentRepository(appDbContext);
            _EmployeeRepository = new EmployeeRepository(appDbContext);
            this.appDbContext = appDbContext;
        }
        public IDepartmentRepository DepartmentRepository => _DepartmentRepository;

        public IEmployeeRepository EmployeeRepository => _EmployeeRepository;

        public async Task<int> saveAsync()
        {
            return await appDbContext.SaveChangesAsync();
        }
    }
}
