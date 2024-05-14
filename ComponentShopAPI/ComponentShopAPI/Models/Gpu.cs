using System.ComponentModel.DataAnnotations;

namespace ComponentShopAPI.Models
{
    public class Gpu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Availability { get; set; }
        public string? Slot { get; set; }
        public int Memory { get; set; }
        public List<string>? Ports { get; set; }
    }
}
