using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MongoRedis.Services.Clients
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> SendAsync(
            string token, 
            string body, 
            IDictionary<string, string> properties, 
            string fullAddress, 
            HttpMethod httpMethod);
    }
}
