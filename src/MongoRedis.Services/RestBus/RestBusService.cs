using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoRedis.Services.Clients;
using MongoRedis.Settings;

namespace MongoRedis.Services.RestBus
{
    public class RestBusService : IRestBusService
    {
        private string fullAddress;
        private string token;

        private readonly IOptions<RestBusSettings> _settings;
        private readonly IHttpClientService _httpClientService;

        public RestBusService(IOptions<RestBusSettings> settings, IHttpClientService httpClientService)
        {
            _settings = settings;
            _httpClientService = httpClientService;
            fullAddress = $"{_settings.Value.Endpoint.Replace("sb://", "https://")}/queuetest1/messages?timeout=60&api-version=2013-08";
            token = GetSASToken("RootManageSharedAccessKey", _settings.Value.SharedAccessKey);
        }
        
        public async Task SendMessageAsync(string content)
        {
            var properties = new Dictionary<string, string>
            {
                ["Priority"] = "High"
            };

            await _httpClientService.SendAsync(token, content, properties, fullAddress, HttpMethod.Post);
        }

        private string GetSASToken(string SASKeyName, string SASKeyValue)
        {
            var endpoint = $"{_settings.Value.Endpoint}/";
            var fromEpochStart = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var expiry = Convert.ToString((int)fromEpochStart.TotalSeconds + 3600);
            var stringToSign = WebUtility.UrlEncode(endpoint) + "\n" + expiry;
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SASKeyValue));

            var signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));
            var sasToken = string.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}&skn={3}",
                WebUtility.UrlEncode(endpoint), WebUtility.UrlEncode(signature), expiry, SASKeyName);
            return sasToken;
        }
    }
}
