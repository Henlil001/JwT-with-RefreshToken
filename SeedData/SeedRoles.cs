using JwT_with_RefreshToken.Common;
using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JwT_with_RefreshToken.SeedData
{
    public static class SeedRoles
    {
        public static async Task InitializeRolesAsync(this IApplicationBuilder app, Roles configRoles)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();

                var roleNames = configRoles
                          .GetType()
                          .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                          .Select(p => p.GetValue(configRoles)?.ToString())
                          .Where(role => !string.IsNullOrWhiteSpace(role))
                          .Distinct();

                foreach (var roleName in roleNames.ToList())
                {
                    bool exists = await context.Roles.AnyAsync(r => r.RoleName == roleName);
                    if (!exists)
                    {
                        context.Roles.Add(new Role { RoleName = roleName });
                    }
                    
                }
                //await context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

          
        }
    }
}
