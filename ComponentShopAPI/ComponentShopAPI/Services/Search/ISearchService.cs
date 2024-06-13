using ComponentShopAPI.Entities;
using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Search
{
    public interface ISearchService
    {
        public List<Product> Search(List<Product> products, ProductGetParameters productQueryParameters);
    }
}
