using ComponentShopAPI.Entities;
using ComponentShopAPI.Repositories;
using ComponentShopAPI.Services.CartManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartManager _cartManager;

        public CartController(ComponentShopDBContext context, UserManager<ApplicationUser> userManager,
            ICartManager cartManager)
        {
            _context = context;
            _userManager = userManager;
            _cartManager = cartManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetCarts()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest(new { message = "Must be logged in to purchase." });
            }

            var cart = await _cartManager.GetCartAsync(user.Id);
            if (cart == null)
            {
                return Ok(new { });
            }

            var result = _cartManager.GetCartDtos(cart);

            return Ok(result);
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

            await _cartManager.RemoveProductFromCartAsync(cart, product, 1);

            return Ok(new { message = $"Removed product {product.Name} from cart for user {user.UserName}." });
        }

        [HttpPut("purchase")]
        public async Task<ActionResult> Purchase()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest(new { message = "Must be logged in to purchase." });
            }

            var cart = await _cartManager.GetCartAsync(user.Id);
            if (cart == null || _cartManager.IsCartEmpty(cart))
            {
                return BadRequest(new { message = $"No products in cart for user {user.UserName}." });
            }

            var total = await _cartManager.GetCartTotalAsync(cart);
            if (user.Credits < total)
            {
                return BadRequest(new { message = "Not enough credits for purchase." });
            }

            await _cartManager.ProcessPurchaseAsync(cart, user);

            return Ok(new { message = "Purchase successful." });
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
