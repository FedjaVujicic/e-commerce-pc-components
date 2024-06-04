namespace ComponentShopAPI.Services.Pagination
{
    public interface IPaginationService<T>
    {
        public List<T> Paginate(List<T> items, int currentPage, int pageSize);
    }
}
