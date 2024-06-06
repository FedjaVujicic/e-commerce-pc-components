using ComponentShopAPI.Dtos;
using ComponentShopAPI.Models;

namespace ComponentShopAPI.Services.ProductDtoFactory
{
    public interface IProductDtoFactory
    {
        ProductDto Create(Product product);
    }
}
