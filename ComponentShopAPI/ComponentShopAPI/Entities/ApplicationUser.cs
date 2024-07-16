using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentShopAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; } = "";

        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; } = "";

        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public double Credits { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; } = "Created";
    }
}
