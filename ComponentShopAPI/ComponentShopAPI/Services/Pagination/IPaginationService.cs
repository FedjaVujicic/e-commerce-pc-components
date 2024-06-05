namespace ComponentShopAPI.Services.Pagination
{
    public interface IPaginationService
    {
        public List<T> Paginate<T>(List<T> items, int currentPage, int pageSize);
    }
}
