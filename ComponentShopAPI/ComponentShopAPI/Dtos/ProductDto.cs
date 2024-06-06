using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ComponentShopAPI.Dtos
{
    [JsonDerivedType(typeof(MonitorDto))]
    [JsonDerivedType(typeof(GpuDto))]
    public abstract class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public bool Availability { get; set; }
        public FileContentResult? ImageFile { get; set; }
    }
}
