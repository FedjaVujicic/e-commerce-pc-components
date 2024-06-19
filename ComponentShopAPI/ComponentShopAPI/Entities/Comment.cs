using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentShopAPI.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string Text { get; set; } = "";

        public string UserId { get; set; } = "";

        public int ProductId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        public Product Product { get; set; } = null!;
    }
}
