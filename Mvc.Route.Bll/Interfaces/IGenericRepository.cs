using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvc.Route.Dal.Models;

namespace Mvc.Route.Bll.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntityInt
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task AddAsync(T entity);
        public void Update(T entity);
        public void Delete(T entity);

    }
}
