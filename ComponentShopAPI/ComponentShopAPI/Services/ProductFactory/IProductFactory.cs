using ComponentShopAPI.Entities;
using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.ProductFactory
{
    public interface IProductFactory
    {
        Product Create(ProductPostParameters productParameters);
    }
}
