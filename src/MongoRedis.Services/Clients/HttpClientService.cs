using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MongoRedis.Services.Clients
{
    public class HttpClientService : IHttpClientService
    {
        HttpClient httpClient = new HttpClient();

        public async Task<HttpResponseMessage> SendAsync(string token, string body, IDictionary<string, string> properties, string fullAddress, HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(fullAddress),
                Method = httpMethod,

            };
            httpClient.DefaultRequestHeaders.Add("Authorization", token);

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    request.Headers.Add(property.Key, property.Value);
                }
            }

            request.Content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("", body) });

            var response = await httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                string error = string.Format("{0} : {1}", response.StatusCode, response.ReasonPhrase);
                throw new Exception(error);
            }

            return response;
        }
    }
}
