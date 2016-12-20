using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoRedis.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(ObjectId id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(ObjectId id, T entity);

        Task<bool> RemoveAsync(ObjectId id);
    }
}
