using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoRedis.Services
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(FilterDefinition<T> filter, T entity);

        Task<bool> RemoveAsync(FilterDefinition<T> filter);
    }
}
