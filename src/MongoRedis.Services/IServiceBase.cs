using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoRedis.Services
{
    public interface IServiceBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(ObjectId id, T entity);

        Task<bool> RemoveAsync(ObjectId id);
    }
}
