using MongoDB.Bson;
using MongoDB.Driver;
using MongoRedis.Entities;
using MongoRedis.Repository.People;
using MongoRedis.Services.RemoteCache;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoRedis.Services.People
{
    public class PeopleService : IPeopleService
    {
        private readonly IRemoteCacheService _remoteCacheService;
        private readonly IPeopleRepository _peopleRepository;

        public PeopleService(IRemoteCacheService remoteCacheService, IPeopleRepository peopleRepository)
        {
            _remoteCacheService = remoteCacheService;
            _peopleRepository = peopleRepository;
        }

        public async Task<Person> AddAsync(Person entity)
        {
            return await _peopleRepository.AddAsync(entity);
        }

        private async Task<IEnumerable<Person>> GetCachedData()
        {
            var isPeopleCollectionCached = await _remoteCacheService.ExistsAsync("people");
            if (isPeopleCollectionCached)
            {
                var peopleFromCache = await _remoteCacheService.GetAsync("people");

                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Person>>(peopleFromCache));
            }

            return null;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var cachedData = await GetCachedData();
            var result = cachedData ?? await _peopleRepository.GetAllAsync();

            if(cachedData == null)
                await _remoteCacheService.SaveAsync("people", JsonConvert.SerializeObject(result));

            return result;
        }

        public async Task<Person> GetByIdAsync(ObjectId id)
        {
            var cachedData = await GetCachedData();
            var result = cachedData.FirstOrDefault(x => x.Id == id.ToString()) ?? await _peopleRepository.GetByIdAsync(id);

            if (cachedData == null)
                await _remoteCacheService.SaveAsync("people", JsonConvert.SerializeObject(result));

            return result;
        }

        public async Task<bool> RemoveAsync(FilterDefinition<Person> filter)
        {
            return await _peopleRepository.RemoveAsync(filter);
        }

        public async Task<Person> UpdateAsync(FilterDefinition<Person> filter, Person entity)
        {
            return await _peopleRepository.UpdateAsync(filter, entity);
        }
    }
}
