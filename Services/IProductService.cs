using JwT_with_RefreshToken.Entities;

namespace JwT_with_RefreshToken.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
    }
}
