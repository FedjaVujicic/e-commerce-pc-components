using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Gpu
{
    public interface IGpuService
    {
        public List<Models.Gpu> Search(List<Models.Gpu> gpus, GpuQueryParameters queryParameters);
    }
}
