using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Database;
using Template.Domain;
using Template.Core.Interfaces.Repository;
using Template.Database.Repository;

namespace Template.WebAPI.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TemplateAPI")));
            services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TemplateContext>();


            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repository
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}