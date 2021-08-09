using Application.ApplicationTask;
using Application.ApplicationUser;
using Application.Authorization;
using Application.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceRegistration
    {
        public static void AddServicesAssembly(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IApplicationTaskService, ApplicationTaskService>();
        }
    }
}