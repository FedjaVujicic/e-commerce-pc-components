using System.ComponentModel.DataAnnotations;

namespace ComponentShopAPI.Models
{
    public class Monitor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Price { get; set; }
        public string? Availability { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
    }
}
