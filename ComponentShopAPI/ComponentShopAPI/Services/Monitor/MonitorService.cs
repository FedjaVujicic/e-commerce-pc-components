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
                queryResults = queryResults.Where(monitor => monitor.Availability == "In Stock");
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
            return queryResults.ToList();
        }

        public List<Models.Monitor> Paginate(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters)
        {
            if (monitors.Count() >= queryParameters.CurrentPage * queryParameters.PageSize)
            {
                return monitors.GetRange((queryParameters.CurrentPage - 1) * queryParameters.PageSize, queryParameters.PageSize);
            }
            else
            {
                return monitors.GetRange((queryParameters.CurrentPage - 1) * queryParameters.PageSize,
                    monitors.Count - (queryParameters.CurrentPage - 1) * queryParameters.PageSize);
            }
        }
    }
}
