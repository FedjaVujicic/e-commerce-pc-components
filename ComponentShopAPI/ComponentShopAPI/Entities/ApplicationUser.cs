using Microsoft.AspNetCore.Identity;

namespace ComponentShopAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public double Credits { get; set; }
    }
}
