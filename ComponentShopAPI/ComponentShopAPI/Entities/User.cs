using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentShopAPI.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public double Credits { get; set; }
        public string ImageName { get; set; } = "";
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
