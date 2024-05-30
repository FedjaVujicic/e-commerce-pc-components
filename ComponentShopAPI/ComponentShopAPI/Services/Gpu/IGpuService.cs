using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Gpu
{
    public interface IGpuService
    {
        public List<Models.Gpu> Search(List<Models.Gpu> gpus, GpuQueryParameters queryParameters);
        public List<Models.Gpu> Paginate(List<Models.Gpu> gpus, GpuQueryParameters queryParameters);
    }
}
