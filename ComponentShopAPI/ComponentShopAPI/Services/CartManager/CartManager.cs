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
            var cartProduct = await GetCartProduct(cart, product);
            if (cartProduct == null)
            {
                cart.Products.Add(product);
                await _context.SaveChangesAsync();
            }

            cartProduct = await GetCartProduct(cart, product);
            cartProduct!.Quantity += 1;
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

        private async Task<CartProduct?> GetCartProduct(Cart cart, Product product)
        {
            return await _context.CartProduct.FirstOrDefaultAsync(cp => cp.CartId == cart.Id && cp.ProductId == product.Id);
        }
    }
}
