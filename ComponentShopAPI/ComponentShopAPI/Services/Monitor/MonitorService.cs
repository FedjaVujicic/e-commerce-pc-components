using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Monitor
{
    public class MonitorService : IMonitorService
    {
        public MonitorService() { }

        public List<Models.Monitor> Search(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters)
        {
            return monitors.Where(monitor => monitor.Name.IndexOf(queryParameters.Name, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
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
