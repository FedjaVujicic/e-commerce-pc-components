using ComponentShopAPI.Entities;
using ComponentShopAPI.Repositories;
using ComponentShopAPI.Services.CartManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartManager _cartManager;

        public CartsController(ComponentShopDBContext context, UserManager<ApplicationUser> userManager,
            ICartManager cartManager)
        {
            _context = context;
            _userManager = userManager;
            _cartManager = cartManager;
        }

        [HttpPut("add")]
        public async Task<ActionResult> AddToCart([FromQuery] int productId)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest(new { message = "Must be logged in to purchase." });
            }

            var product = await _cartManager.GetProductByIdAsync(productId);
            if (product == null)
            {
                return BadRequest(new { message = $"Product with the id {productId} does not exist." });
            }

            var cart = await _cartManager.GetCartAsync(user.Id);
            if (cart == null)
            {
                cart = await _cartManager.CreateCartAsync(user.Id);
            }

            await _cartManager.AddProductToCartAsync(cart, product);

            return Ok(new { message = $"Added product {product.Name} to cart for user {user.UserName}." });
        }

        [HttpPut("remove")]
        public async Task<ActionResult> RemoveFromCart([FromQuery] int productId)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest(new { message = "Must be logged in to purchase." });
            }

            var product = await _cartManager.GetProductByIdAsync(productId);
            if (product == null)
            {
                return BadRequest(new { message = $"Product with the id {productId} does not exist." });
            }

            var cart = await _cartManager.GetCartAsync(user.Id);
            if (cart == null)
            {
                return BadRequest(new { message = $"No products in cart for user {user.UserName}" });
            }

            await _cartManager.RemoveProductFromCartAsync(cart, product);

            return Ok(new { message = $"Removed product {product.Name} from cart for user {user.UserName}." });
        }

        private async Task<ApplicationUser?> GetCurrentUserAsync()
        {
            if (User.Identity == null || User.Identity.Name == null)
            {
                return null;
            }

            var username = User.Identity.Name;
            return await _userManager.FindByNameAsync(username);
        }
    }
}
