using JwT_with_RefreshToken.DataAcces;
using JwT_with_RefreshToken.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JwT_with_RefreshToken.Services
{
    public class ProductService : IProductService
    {
        readonly AuthenticationDbContext _context;
        public ProductService(AuthenticationDbContext context)
        {
            _context = context;
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _context.Products.ToListAsync();
        }
    }
}
