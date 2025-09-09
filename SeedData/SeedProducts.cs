using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwT_with_RefreshToken.SeedData
{
    public static class SeedProducts
    {
        public static async Task AddProductsAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AuthenticationDbContext>();

            var products = new List<Product>
            {
                new() { Name = "Product 1", Description = "This is product no 1" },
                new() { Name = "Product 2", Description = "This is product no 2" },
                new() { Name = "Product 4", Description = "This is product no 3" },
                new() { Name = "Product 4", Description = "This is product no 4" }
            };
            foreach (var product in products)
            {
                bool exists = await context.Products.AnyAsync(p => p.Name == product.Name);
                if (!exists)
                {
                    context.Products.Add(product);
                }

            }
            await context.SaveChangesAsync();

        }

    }
}
