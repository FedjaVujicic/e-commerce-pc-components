using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Monitor
{
    public class MonitorService : IMonitorService
    {
        public MonitorService() { }

        public List<Models.Monitor> Search(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters)
        {
            return monitors.Where(monitor => monitor.Name.IndexOf(queryParameters.searchParam, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
        }

        public List<Models.Monitor> Paginate(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters)
        {
            if (monitors.Count() >= queryParameters.currentPage * queryParameters.pageSize)
            {
                return monitors.GetRange((queryParameters.currentPage - 1) * queryParameters.pageSize, queryParameters.pageSize);
            }
            else
            {
                return monitors.GetRange((queryParameters.currentPage - 1) * queryParameters.pageSize,
                    monitors.Count - (queryParameters.currentPage - 1) * queryParameters.pageSize);
            }
        }
    }
}
