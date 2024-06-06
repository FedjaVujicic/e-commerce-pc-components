using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;

namespace ComponentShopAPI.Dtos
{
    public class GpuDto : ProductDto
    {
        public string Slot { get; set; }
        public int Memory { get; set; }
        public List<string> Ports { get; set; }

        public GpuDto(Gpu gpu, IImageService imageService)
        {
            Id = gpu.Id;
            Name = gpu.Name;
            Price = gpu.Price;
            Availability = gpu.Quantity > 0;
            ImageFile = imageService.Download(gpu.ImageName);
            Slot = gpu.Slot;
            Memory = gpu.Memory;
            Ports = gpu.Ports;
        }
    }
}
