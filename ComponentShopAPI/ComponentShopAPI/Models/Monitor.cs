using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComponentShopAPI.Models
{
    public class Monitor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
        public string ImageName { get; set; } = "";
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
