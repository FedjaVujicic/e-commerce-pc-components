using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Monitor
{
    public interface IMonitorService
    {
        public List<Models.Monitor> Search(List<Models.Monitor> monitors, MonitorQueryParameters queryParameters);
    }
}
