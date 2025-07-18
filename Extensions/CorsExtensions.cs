using JwT_with_RefreshToken.Common;
using Microsoft.Extensions.Options;

namespace JwT_with_RefreshToken.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCorsPolicyExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => 
            options.AddPolicy(name: configuration["AppSettings:CorsPolicyName"]!, 
            policy => policy
            .AllowCredentials()
            .AllowAnyHeader()
            .WithOrigins(configuration["AppSettings:ClientDomain"]!)
            .WithMethods("POST")));

            return services;
        
        }
    }
}
