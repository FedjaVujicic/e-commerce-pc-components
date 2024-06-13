using ComponentShopAPI.Entities;
using ComponentShopAPI.Helpers;
using Monitor = ComponentShopAPI.Entities.Monitor;

namespace ComponentShopAPI.Services.ProductFactory
{
    public class ProductFactory : IProductFactory
    {
        public Product Create(ProductPostParameters productParameters)
        {
            if (productParameters.Category == "gpu")
            {
                return new Gpu(
                    productParameters.Id,
                    productParameters.Name,
                    productParameters.Price,
                    productParameters.Quantity,
                    productParameters.ImageName,
                    productParameters.ImageFile,
                    productParameters.Slot,
                    productParameters.Memory,
                    productParameters.Ports
                    );
            }
            else if (productParameters.Category == "monitor")
            {
                return new Monitor(
                    productParameters.Id,
                    productParameters.Name,
                    productParameters.Price,
                    productParameters.Quantity,
                    productParameters.ImageName,
                    productParameters.ImageFile,
                    productParameters.Size,
                    productParameters.Width,
                    productParameters.Height,
                    productParameters.RefreshRate
                    );

            }
            else
            {
                throw new ArgumentException($"Invalid category {productParameters.Category}");
            }
        }
    }
}
