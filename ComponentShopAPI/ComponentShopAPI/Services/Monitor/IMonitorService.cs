using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Monitor
{
    public interface IMonitorService
    {
        public List<Models.Monitor> Search(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters);
        public List<Models.Monitor> Paginate(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters);
    }
}
