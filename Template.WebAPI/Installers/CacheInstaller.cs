using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Template.Core.Interfaces;
using Template.Core.Settings;
using Template.Services;

namespace Template.WebAPI.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = configuration
                .GetSection("ResponseCacheService")
                .Get<RedisCacheSettings>();

            services.AddSingleton(redisCacheSettings);

            if(!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(redisCacheSettings.ConnectionString));
            services.AddStackExchangeRedisCache(i => i.Configuration = redisCacheSettings.ConnectionString);
           
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
