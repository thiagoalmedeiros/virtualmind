using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Virtualmind.Api.Data.Repository.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(string Id);
        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> FindByIdAsync(string Id);
        void Update(T entity);

        Task<IEnumerable<T>> FindAll();
    }
}
