using System.Threading.Tasks;

namespace MongoRedis.Services.RemoteCache
{
    public interface IRemoteCacheService
    {
        Task<bool> ExistsAsync(string key);

        Task SaveAsync(string key, string value);

        Task<string> GetAsync(string key);

        Task RemoveAsync(string key);

        Task<string> DropAndCreateAsync(string key, string value);

        //void Clear();
    }
}
