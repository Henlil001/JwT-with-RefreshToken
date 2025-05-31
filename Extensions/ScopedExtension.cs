using JwT_with_RefreshToken.AuthService;

namespace JwT_with_RefreshToken.Extensions
{
    public static class ScopedExtension
    {
        public static IServiceCollection AddScopedExtension(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
