using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComponentShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpGet("signOut")]
        public async Task<IActionResult> SignOutUser()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("getAllRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        [HttpPost("createRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                return BadRequest(new { error = "Role already exists." });
            }
            await _roleManager.CreateAsync(new IdentityRole(roleName));
            return Ok();
        }

        [HttpPost("deleteRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return BadRequest(new { error = "Role does not exist." });
            }
            await _roleManager.DeleteAsync(role);
            return Ok();
        }

        [HttpPost("setUserRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { error = "User does not exist." });
            }
            await _userManager.AddToRoleAsync(user, roleName);
            return Ok();
        }

        [HttpDelete("removeUserRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveAdmin(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(new { error = "User does not exist." });
            }
            await _userManager.RemoveFromRoleAsync(user, roleName);

            return Ok();
        }

        [HttpGet("authStatus")]
        public ActionResult GetAuthStatus()
        {
            if (User.Identity == null)
            {
                return Ok();
            }
            return Ok(new { isLoggedIn = User.Identity.IsAuthenticated });
        }
    }
}
