using ComponentShopAPI.Entities;
using ComponentShopAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Services.CartManager
{
    public class CartManager : ICartManager
    {
        private readonly ComponentShopDBContext _context;

        public CartManager(ComponentShopDBContext context)
        {
            _context = context;
        }

        public async Task AddProductToCartAsync(Cart cart, Product product)
        {
            cart.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }
            return cart;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
