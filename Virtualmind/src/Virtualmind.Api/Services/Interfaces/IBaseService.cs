using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Virtualmind.Api.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(string publicId);
        Task<T> FindByIdAsync(string publicId);
        Task UpdateAsync(T entity);

        Task<IEnumerable<T>> FindAll();
    }
}
