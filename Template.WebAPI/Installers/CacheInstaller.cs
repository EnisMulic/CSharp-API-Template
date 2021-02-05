using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template.Core.Interfaces;
using Template.Core.Settings;
using Template.Services;

namespace Template.WebAPI.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSettings);

            services.AddSingleton(redisCacheSettings);

            if(!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddStackExchangeRedisCache(options => 
                options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
