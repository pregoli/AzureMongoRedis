using Microsoft.Extensions.Options;
using MongoRedis.Settings;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MongoRedis.Services.RemoteCache
{
    public class RemoteCacheService : IRemoteCacheService
    {
        private readonly IDatabase _cache;
        private static ConnectionMultiplexer _connectionMultiplexer;

        public RemoteCacheService(IOptions<RedisCacheSettings> settings)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(settings.Value.Configuration);
            _cache = _connectionMultiplexer.GetDatabase();
        }

        public void Clear()
        {
            var endpoints = _connectionMultiplexer.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _connectionMultiplexer.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _cache.KeyExistsAsync(key);
        }

        public async Task<string> GetAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public async Task SaveAsync(string key, string value)
        {
            var ts = TimeSpan.FromMinutes(10);
            await _cache.StringSetAsync(key, value, ts);
        }

        public async Task<string> DropAndCreateAsync(string key, string value)
        {
            await this.RemoveAsync(key);
            await this.SaveAsync(key, value);

            return await this.GetAsync(key);
        }
    }
}
