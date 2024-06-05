using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;

namespace ComponentShopAPI.Dtos
{
    public class GpuDto : ProductDto
    {
        public string Slot { get; set; }
        public int Memory { get; set; }
        public List<string> Ports { get; set; }

        public GpuDto(Gpu gpu, IImageService imageService) : base(gpu, imageService)
        {
            Slot = gpu.Slot;
            Memory = gpu.Memory;
            Ports = gpu.Ports;
        }
    }
}
