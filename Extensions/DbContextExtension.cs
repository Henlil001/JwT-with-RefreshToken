using JwT_with_RefreshToken.Common;
using JwT_with_RefreshToken.DataAcces;
using Microsoft.EntityFrameworkCore;

namespace JwT_with_RefreshToken.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContextExtension(this IServiceCollection services, AppSettings appsettings)
        {
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(appsettings.DbConnString));
            return services;
        }
    }
}
