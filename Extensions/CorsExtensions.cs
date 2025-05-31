using JwT_with_RefreshToken.Common;
using Microsoft.Extensions.Options;

namespace JwT_with_RefreshToken.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicyExtension(this IServiceCollection services, AppSettings appsettings)
        {
            services.AddCors(options => 
            options.AddPolicy(name: appsettings.CorsPolicyName, 
            policy => policy
            .AllowCredentials()
            .AllowAnyHeader()
            .WithOrigins(appsettings.ClientDomain)
            .WithMethods("POST")));

            return services;
        
        }
    }
}
