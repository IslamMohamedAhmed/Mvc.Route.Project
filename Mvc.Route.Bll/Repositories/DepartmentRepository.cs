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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public int Add(Department department)
        {
            appDbContext.Departments.Add(department);
            return  appDbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            appDbContext.Departments.Remove(department);
            return appDbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return appDbContext.Departments.ToList();
        }

        public Department GetById(int? id)
        {
            return appDbContext.Departments.Find(id);
        }

        public int Update(Department department)
        {
            appDbContext.Departments.Update(department);
            return appDbContext.SaveChanges();
        }
    }
}
