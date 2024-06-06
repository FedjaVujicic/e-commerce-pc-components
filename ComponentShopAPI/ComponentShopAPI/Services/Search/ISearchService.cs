using ComponentShopAPI.Helpers;
using ComponentShopAPI.Models;

namespace ComponentShopAPI.Services.Search
{
    public interface ISearchService
    {
        public List<Product> Search(List<Product> products, ProductQueryParameters productQueryParameters);
    }
}
