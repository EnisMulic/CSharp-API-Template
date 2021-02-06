using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Template.Database;
using Template.WebAPI.HealthChecks;

namespace Template.WebAPI.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<TemplateContext>()
                .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
