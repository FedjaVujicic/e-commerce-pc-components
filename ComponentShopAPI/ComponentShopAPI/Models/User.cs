using System.ComponentModel.DataAnnotations;

namespace ComponentShopAPI.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Role { get; set; } = "";
    }
}
