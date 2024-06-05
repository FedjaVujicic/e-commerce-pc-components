using ComponentShopAPI.Services.Image;
using Monitor = ComponentShopAPI.Models.Monitor;

namespace ComponentShopAPI.Dtos
{
    public class MonitorDto : ProductDto
    {
        public double Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
        public MonitorDto(Monitor monitor, IImageService imageService) : base(monitor, imageService)
        {
            Size = monitor.Size;
            Width = monitor.Width;
            Height = monitor.Height;
            RefreshRate = monitor.RefreshRate;
        }
    }
}
