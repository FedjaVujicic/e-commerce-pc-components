namespace ComponentShopAPI.Services.Pagination
{
    public class PaginationService<T> : IPaginationService<T>
    {
        public List<T> Paginate(List<T> items, int currentPage, int pageSize)
        {
            int startIndex = (currentPage - 1) * pageSize;
            int count = Math.Min(pageSize, items.Count - startIndex);

            return items.GetRange(startIndex, count);
        }
    }
}
