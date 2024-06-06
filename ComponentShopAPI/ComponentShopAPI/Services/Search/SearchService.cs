using ComponentShopAPI.Helpers;
using ComponentShopAPI.Models;
using Monitor = ComponentShopAPI.Models.Monitor;

namespace ComponentShopAPI.Services.Search
{
    public class SearchService : ISearchService
    {
        public List<Product> Search(List<Product> products, ProductQueryParameters productQueryParameters)
        {
            if (productQueryParameters.Category == "gpu")
            {
                return SearchGpus(products, productQueryParameters);
            }
            else if (productQueryParameters.Category == "monitor")
            {
                return SearchMonitors(products, productQueryParameters);
            }
            else if (productQueryParameters.Category == "")
            {
                return products;
            }
            throw new BadHttpRequestException($"Invalid category {productQueryParameters.Category}");
        }

        private List<Product> SearchMonitors(List<Product> products, ProductQueryParameters productQueryParameters)
        {
            var queryResults = products.Select(product => (Monitor)product);
            queryResults = queryResults.Where(monitor =>
                monitor.Name.IndexOf(productQueryParameters.Name, StringComparison.OrdinalIgnoreCase) >= 0 &&
                monitor.Price >= productQueryParameters.PriceLow &&
                monitor.Price < productQueryParameters.PriceHigh &&
                monitor.Size >= productQueryParameters.SizeLow &&
                monitor.Size < productQueryParameters.SizeHigh
                );
            if (productQueryParameters.AvailableOnly)
            {
                queryResults = queryResults.Where(monitor => monitor.Quantity > 0);
            }
            if (productQueryParameters.Resolution != "")
            {
                queryResults = queryResults.Where(monitor =>
                $"{monitor.Width}x{monitor.Height}" == productQueryParameters.Resolution
                );
            }
            if (productQueryParameters.RefreshRate != -1)
            {
                queryResults = queryResults.Where(monitor => monitor.RefreshRate == productQueryParameters.RefreshRate);
            }
            if (productQueryParameters.Sort == "Ascending")
            {
                queryResults = queryResults.OrderBy(monitor => monitor.Price);
            }
            if (productQueryParameters.Sort == "Descending")
            {
                queryResults = queryResults.OrderByDescending(monitor => monitor.Price);
            }
            return queryResults.Select(monitor => (Product)monitor).ToList();
        }

        private List<Product> SearchGpus(List<Product> products, ProductQueryParameters productQueryParameters)
        {
            var queryResults = products.Select(product => (Gpu)product);
            queryResults = queryResults.Where(gpu =>
                    gpu.Name.IndexOf(productQueryParameters.Name, StringComparison.OrdinalIgnoreCase) >= 0 &&
                    gpu.Price >= productQueryParameters.PriceLow &&
                    gpu.Price < productQueryParameters.PriceHigh
                    );
            if (productQueryParameters.AvailableOnly)
            {
                queryResults = queryResults.Where(gpu => gpu.Quantity > 0);
            }
            if (productQueryParameters.Memory != -1)
            {
                queryResults = queryResults.Where(gpu => gpu.Memory == productQueryParameters.Memory);
            }
            if (productQueryParameters.Slot != "")
            {
                queryResults = queryResults.Where(gpu => gpu.Slot == productQueryParameters.Slot);
            }
            if (productQueryParameters.Ports.Count > 0)
            {
                foreach (var port in productQueryParameters.Ports)
                {
                    queryResults = queryResults.Where(gpu => gpu.Ports.Contains(port));
                }
            }
            if (productQueryParameters.Sort == "Ascending")
            {
                queryResults = queryResults.OrderBy(gpu => gpu.Price);
            }
            if (productQueryParameters.Sort == "Descending")
            {
                queryResults = queryResults.OrderByDescending(gpu => gpu.Price);
            }

            return queryResults.Select(gpu => (Product)gpu).ToList();
        }
    }
}

