using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Contracts.V1.Requests;
using Template.Contracts.V1.Responses;
using Template.Core.Interfaces.Services;
using Template.Services;

namespace Template.WebAPI.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Auth
            services.AddScoped<IAuthService, AuthService>();

            // User
            services.AddScoped<
                ICRUDService<UserResponse, UserSearchRequest, UserInsertRequest, UserUpdateRequest>,
                UserService>();

            // Role
            services.AddScoped<IBaseService<RoleResponse, RoleSearchRequest>, RoleService>();
        }
    }
}
