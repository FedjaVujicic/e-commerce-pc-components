using ComponentShopAPI.Dtos;
using ComponentShopAPI.Entities;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Registration successful" });
            }
            return BadRequest(new { result.Errors });
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

        [HttpGet("userInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (User.Identity == null)
            {
                return NoContent();
            }
            if (User.Identity.Name == null)
            {
                return NoContent();
            }

            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NoContent();
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new
            {
                username = user.UserName,
                role = roles.FirstOrDefault()
            });
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
    }
}
