using ComponentShopAPI.Entities;

namespace ComponentShopAPI.Services.CartManager
{
    public interface ICartManager
    {
        public Task AddProductToCartAsync(Cart cart, Product product);

        public Task RemoveProductFromCartAsync(Cart cart, Product product);

        public Task<Cart> CreateCartAsync(string userId);

        public Task<Cart?> GetCartAsync(string userId);

        public Task<Product?> GetProductByIdAsync(int id);
    }
}
