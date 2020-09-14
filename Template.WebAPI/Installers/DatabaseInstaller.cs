using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces;
using Template.Database;
using Template.Domain;
using Template.Services;

namespace Template.WebAPI.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TemplateAPI")));
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TemplateContext>();


            // Auth
            services.AddScoped<IUserAccountService, UserAccountService>();

            // User
            services.AddScoped<ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>, UserService>();
        }
    }
}