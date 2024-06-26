using ComponentShopAPI.Dtos;
using ComponentShopAPI.Entities;
using ComponentShopAPI.Helpers;
using ComponentShopAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ComponentShopDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(ComponentShopDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetCommentsForProduct(int productId)
        {
            var comments = await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();

            var commentDtos = new List<CommentDto>();
            foreach (var comment in comments)
            {
                var user = await _userManager.FindByIdAsync(comment.UserId);
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (user == null || user.UserName == null)
                {
                    throw new BadHttpRequestException("User not found");
                }
                if (product == null)
                {
                    throw new BadHttpRequestException("Product not found");
                }

                var commentDto = new CommentDto(user.FirstName, user.LastName, product.Name, comment.Text);
                commentDtos.Add(commentDto);
            }

            return Ok(commentDtos);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentPostParameters parameters)
        {
            var user = await GetCurrentUserAsync();

            if (user == null)
            {
                return BadRequest(new { message = "Must be logged in to comment" });
            }

            var userId = user.Id;
            var comment = new Comment
            {
                UserId = userId,
                ProductId = parameters.ProductId,
                Text = parameters.Text
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return StatusCode(204);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
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
