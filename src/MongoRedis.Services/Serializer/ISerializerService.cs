using System.Threading.Tasks;

namespace MongoRedis.Services.Serializer
{
    public interface ISerializerService
    {
        Task<T> DeSerializeEntity<T>(string jsonEntity);
        Task<string> SerializeEntity<T>(T entity) where T : class;
    }
}