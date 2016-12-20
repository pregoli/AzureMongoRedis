using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoRedis.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoRedis.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected IMongoCollection<T> _collection;

        public Repository(IOptions<MongoDbSettings> settings, string collection)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.Database);
                _collection = _database.GetCollection<T>(collection);
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find<T>(new BsonDocument()).ToListAsync();
        }

        public async Task<T> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find<T>(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveAsync(FilterDefinition<T> filter)
        {
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<T> UpdateAsync(FilterDefinition<T> filter, T entity)
        {
            await _collection.ReplaceOneAsync(filter, entity,
            new UpdateOptions { IsUpsert = true });

            return entity;
        }
    }
}
