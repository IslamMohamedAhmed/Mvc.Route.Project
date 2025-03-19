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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntityInt
    {
        private protected readonly AppDbContext appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddAsync(T entity)
        {
            await appDbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            appDbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await appDbContext.Employees.Include(i=>i.WorkFor).ToListAsync();
            }
            return await appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await appDbContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            appDbContext.Set<T>().Update(entity);
            
        }
    }
}
