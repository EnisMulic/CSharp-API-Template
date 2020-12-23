using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.EmailService;

namespace Template.WebAPI.Installers
{
    public class EmailInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
