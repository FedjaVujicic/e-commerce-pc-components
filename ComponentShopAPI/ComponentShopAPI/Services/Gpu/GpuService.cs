using ComponentShopAPI.Helpers;

namespace ComponentShopAPI.Services.Gpu
{
    public class GpuService : IGpuService
    {
        public GpuService() { }

        public List<Models.Gpu> Search(List<Models.Gpu> gpus, GpuQueryParameters queryParameters)
        {
            return gpus.Where(gpu => gpu.Name.IndexOf(queryParameters.Name, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
        }

        public List<Models.Gpu> Paginate(List<Models.Gpu> gpus, GpuQueryParameters queryParameters)
        {
            if (gpus.Count() >= queryParameters.CurrentPage * queryParameters.PageSize)
            {
                return gpus.GetRange((queryParameters.CurrentPage - 1) * queryParameters.PageSize, queryParameters.PageSize);
            }
            else
            {
                return gpus.GetRange((queryParameters.CurrentPage - 1) * queryParameters.PageSize,
                    gpus.Count - (queryParameters.CurrentPage - 1) * queryParameters.PageSize);
            }
        }
    }
}
