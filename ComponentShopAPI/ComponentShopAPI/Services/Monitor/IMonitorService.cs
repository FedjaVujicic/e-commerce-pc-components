using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.MonitorSearch
{
    public interface IMonitorService
    {
        public List<Entities.Monitor> Search(List<Entities.Monitor> monitors, MonitorQueryParameters queryParameters);
    }
}
