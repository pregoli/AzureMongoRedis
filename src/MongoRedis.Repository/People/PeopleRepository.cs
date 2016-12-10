using Microsoft.Extensions.Options;
using MongoRedis.Entities;
using MongoRedis.Settings;

namespace MongoRedis.Repository.People
{
    public class PeopleRepository : Repository<Person>, IPeopleRepository
    {
        public PeopleRepository(IOptions<MongoDbSettings> settings)
            : base(settings, "People")
        {
        }
    }
}
