using Microsoft.AspNetCore.Identity;

namespace ComponentShopAPI.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public double Credits { get; set; }
    }
}
