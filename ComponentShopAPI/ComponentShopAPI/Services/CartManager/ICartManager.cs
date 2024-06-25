using ComponentShopAPI.Entities;

namespace ComponentShopAPI.Services.CartManager
{
    public interface ICartManager
    {
        public Task AddProductToCartAsync(Cart cart, Product product);

        public Task RemoveProductFromCartAsync(Cart cart, Product product, int quantity);

        public Task<Cart> CreateCartAsync(string userId);

        public Task<Cart?> GetCartAsync(string userId);

        public Task<Product?> GetProductByIdAsync(int id);

        public bool IsCartEmpty(Cart cart);

        public Task<double> GetCartTotalAsync(Cart cart);

        public Task ProcessPurchaseAsync(Cart cart, ApplicationUser user);

    }
}
