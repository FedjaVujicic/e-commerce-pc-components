using ComponentShopAPI.Dtos;
using ComponentShopAPI.Models;
using ComponentShopAPI.Services.Image;
using Monitor = ComponentShopAPI.Models.Monitor;

namespace ComponentShopAPI.Services.ProductDtoFactory
{
    public class ProductDtoFactory : IProductDtoFactory
    {
        private readonly IImageService _imageService;
        public ProductDtoFactory(IImageService imageService)
        {
            _imageService = imageService;
        }
        public ProductDto Create(Product product)
        {
            if (product is Monitor monitor)
            {
                return new MonitorDto(monitor, _imageService);
            }
            else if (product is Gpu gpu)
            {
                return new GpuDto(gpu, _imageService);
            }
            else
            {
                throw new ArgumentException("Unknown product type");
            }
        }
    }
}
