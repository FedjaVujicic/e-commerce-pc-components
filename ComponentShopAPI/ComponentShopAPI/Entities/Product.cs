using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ComponentShopAPI.Entities
{
    [JsonDerivedType(typeof(Monitor))]
    [JsonDerivedType(typeof(Gpu))]
    public abstract class Product
    {
        protected Product() { }
        protected Product(int id, string name, double price, int quantity, string imageName, IFormFile? imageFile)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
            ImageName = imageName;
            ImageFile = imageFile;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; } = "";
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
