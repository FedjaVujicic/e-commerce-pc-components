using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Gpu
{
    public class GpuService : IGpuService
    {
        public GpuService() { }

        public List<Models.Gpu> Search(List<Models.Gpu> gpus, GpuQueryParameters queryParameters)
        {
            var queryResults = gpus.Where(gpu =>
                gpu.Name.IndexOf(queryParameters.Name, StringComparison.OrdinalIgnoreCase) >= 0 &&
                gpu.Price >= queryParameters.PriceLow &&
                gpu.Price < queryParameters.PriceHigh
                );
            if (queryParameters.AvailableOnly)
            {
                queryResults = queryResults.Where(gpu => gpu.Availability == "In Stock");
            }
            if (queryParameters.Memory != -1)
            {
                queryResults = queryResults.Where(gpu => gpu.Memory == queryParameters.Memory);
            }
            if (queryParameters.Slot != "")
            {
                queryResults = queryResults.Where(gpu => gpu.Slot == queryParameters.Slot);
            }
            if (queryParameters.Ports.Count > 0)
            {
                foreach (var port in queryParameters.Ports)
                {
                    queryResults = queryResults.Where(gpu => gpu.Ports.Contains(port));
                }
            }
            return queryResults.ToList();
        }
    }
}
