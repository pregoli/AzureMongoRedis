using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoRedis.Repository.People;
using MongoRedis.Services.Clients;
using MongoRedis.Services.People;
using MongoRedis.Services.RemoteCache;
using MongoRedis.Services.RestBus;
using MongoRedis.Settings;

namespace MongoRedis.IoC
{
    public static class Configuration
    {
        public static void RegisterTypes(IServiceCollection services, IConfigurationRoot configuration)
        {
            // Add MongoDbConnection settings
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration.GetSection("MongoConfiguration:ConnectionString").Value;
                options.Database = configuration.GetSection("MongoConfiguration:Database").Value;
            });

            // Add RedisCache settings
            services.Configure<RedisCacheSettings>(options =>
            {
                options.Configuration = configuration.GetSection("RedisConfiguration:Configuration").Value;
            });

            // Add Azure service bus settings
            services.Configure<RestBusSettings>(options =>
            {
                options.ConnectionString = configuration.GetSection("RestBusConfiguration:ConnectionString").Value;
                options.Endpoint = configuration.GetSection("RestBusConfiguration:Endpoint").Value;
                options.SharedAccessKeyName = configuration.GetSection("RestBusConfiguration:SharedAccessKeyName").Value;
                options.SharedAccessKey = configuration.GetSection("RestBusConfiguration:SharedAccessKey").Value;
            });

            //registering my repos
            services.AddScoped<IPeopleRepository, PeopleRepository>();

            //registering my services
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IRestBusService, RestBusService>();
            services.AddScoped<IRemoteCacheService, RemoteCacheService>();
            services.AddScoped<IPeopleService, PeopleService>();
        }
    }
}
