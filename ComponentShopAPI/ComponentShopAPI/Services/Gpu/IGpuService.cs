using ComponentShopAPI.Entities;
using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.GpuSearch
{
    public interface IGpuService
    {
        public List<Gpu> Search(List<Gpu> gpus, GpuQueryParameters queryParameters);
    }
}
