using JwT_with_RefreshToken.Common;
using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JwT_with_RefreshToken.SeedData
{
    public static class SeedRoles
    {
        public static async Task InitializeRolesAsync(this IApplicationBuilder app, List<string> roleNames)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();

            foreach (var roleName in roleNames)
            {
                bool exists = await context.Roles.AnyAsync(r => r.RoleName == roleName);
                if (!exists)
                {
                    context.Roles.Add(new Role { RoleName = roleName });
                }

            }
            await context.SaveChangesAsync();



        }
    }
}
