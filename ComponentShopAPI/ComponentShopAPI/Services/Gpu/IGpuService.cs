using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.GpuSearch
{
    public interface IGpuService
    {
        public List<Models.Gpu> Search(List<Models.Gpu> gpus, GpuQueryParameters queryParameters);
    }
}
