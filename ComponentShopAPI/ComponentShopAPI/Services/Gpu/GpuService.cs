using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Gpu
{
    public class GpuService : IGpuService
    {
        public GpuService() { }

        public List<Models.Gpu> Search(List<Models.Gpu> gpus, GpuQueryParameters queryParameters)
        {
            return gpus.Where(gpu => gpu.Name.IndexOf(queryParameters.searchParam, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
        }

        public List<Models.Gpu> Paginate(List<Models.Gpu> gpus, GpuQueryParameters queryParameters)
        {
            if (gpus.Count() >= queryParameters.currentPage * queryParameters.pageSize)
            {
                return gpus.GetRange((queryParameters.currentPage - 1) * queryParameters.pageSize, queryParameters.pageSize);
            }
            else
            {
                return gpus.GetRange((queryParameters.currentPage - 1) * queryParameters.pageSize,
                    gpus.Count - (queryParameters.currentPage - 1) * queryParameters.pageSize);
            }
        }
    }
}
