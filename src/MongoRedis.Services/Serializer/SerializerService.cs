using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MongoRedis.Services.Serializer
{
    public class SerializerService
    {
        public async Task<string> SerializeEntity<T>(T entity) where T : class
        {
            return await Task.Factory.StartNew(() => JsonConvert.SerializeObject(entity));
        }

        public async Task<T> DeSerializeEntity<T>(string jsonEntity)
        {
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(jsonEntity));
        }
    }
}
