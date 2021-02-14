using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Virtualmind.Api.Data.Model;
using Virtualmind.Api.Data.Repository.IRepository;
using Virtualmind.Api.Model;

namespace Virtualmind.Api.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected DataContext DataContext { get; }

        public BaseRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            var added = await DataContext.Set<T>().AddAsync(entity);

            return added.Entity;
        }

        public async Task DeleteAsync(string Id)
        {
            var existing = await FindByIdAsync(Id);
            if (existing != null)
            {
                DataContext.Set<T>().Remove(existing);
            }
        }

        public virtual async Task<T> FindByIdAsync(string Id)
        {
            return await DataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> predicate)
        {
            return await DataContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await DataContext.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
            DataContext.Set<T>().Update(entity);
        }
    }
}
