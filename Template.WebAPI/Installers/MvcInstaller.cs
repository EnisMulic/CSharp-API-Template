using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Template.WebAPI.Settings;
using Template.Services;
using Microsoft.AspNetCore.Authorization;
using Template.Authorization.Constants;
using Template.Authorization.Requirements;
using Template.Authorization.Handlers;
using Template.Core.Interfaces;
using Newtonsoft.Json.Converters;

namespace Template.WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public string IsTemplatePolicy { get; private set; }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvcCore().AddApiExplorer().AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors();

            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.TemplatePolicy, policy =>
                {
                    policy.AddRequirements(new TemplateRequirement("template.com"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, TemplateHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), request.Path.ToUriComponent());
                return new UriService(absoluteUri);
            });
        }
    }
}