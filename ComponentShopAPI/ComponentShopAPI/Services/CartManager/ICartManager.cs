using ComponentShopAPI.Entities;

namespace ComponentShopAPI.Services.CartManager
{
    public interface ICartManager
    {
        public Task AddProductToCartAsync(Cart cart, Product product);

        public Task<Cart> GetOrCreateCartAsync(string userId);

        public Task<Product?> GetProductByIdAsync(int id);
    }
}
