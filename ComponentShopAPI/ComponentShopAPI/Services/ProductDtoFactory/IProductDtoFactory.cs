using ComponentShopAPI.Dtos;
using ComponentShopAPI.Entities;

namespace ComponentShopAPI.Services.ProductDtoFactory
{
    public interface IProductDtoFactory
    {
        ProductDto Create(Product product);
    }
}
