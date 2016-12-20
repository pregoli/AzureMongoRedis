using System.Threading.Tasks;

namespace MongoRedis.Services.RestBus
{
    public interface IRestBusService
    {
        Task SendMessageAsync(string content);
    }
}
