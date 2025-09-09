using JwT_with_RefreshToken.AuthService;
using JwT_with_RefreshToken.Services;

namespace JwT_with_RefreshToken.Extensions
{
    public static class ScopedExtension
    {
        public static IServiceCollection AddScopedExtension(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
