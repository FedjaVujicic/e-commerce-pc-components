using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Monitor
{
    public class MonitorService : IMonitorService
    {
        public MonitorService() { }

        public List<Models.Monitor> Search(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters)
        {
            var queryResults = monitors.Where(monitor =>
                monitor.Name.IndexOf(queryParameters.Name, StringComparison.OrdinalIgnoreCase) >= 0 &&
                monitor.Price >= queryParameters.PriceLow &&
                monitor.Price < queryParameters.PriceHigh &&
                monitor.Size >= queryParameters.SizeLow &&
                monitor.Size < queryParameters.SizeHigh
                );
            if (queryParameters.AvailableOnly)
            {
                queryResults = queryResults.Where(monitor => monitor.Quantity > 0);
            }
            if (queryParameters.Resolution != "")
            {
                queryResults = queryResults.Where(monitor =>
                $"{monitor.Width}x{monitor.Height}" == queryParameters.Resolution
                );
            }
            if (queryParameters.RefreshRate != -1)
            {
                queryResults = queryResults.Where(monitor => monitor.RefreshRate == queryParameters.RefreshRate);
            }
            if (queryParameters.Sort == "Ascending")
            {
                queryResults = queryResults.OrderBy(monitor => monitor.Price);
            }
            if (queryParameters.Sort == "Descending")
            {
                queryResults = queryResults.OrderByDescending(monitor => monitor.Price);
            }
            return queryResults.ToList();
        }
    }
}
