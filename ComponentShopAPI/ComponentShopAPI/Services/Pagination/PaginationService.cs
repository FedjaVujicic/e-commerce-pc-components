namespace ComponentShopAPI.Services.Pagination
{
    public class PaginationService : IPaginationService
    {
        public List<T> Paginate<T>(List<T> items, int currentPage, int pageSize)
        {
            if (items.Count == 0)
            {
                return items;
            }

            int startIndex = (currentPage - 1) * pageSize;
            int count = Math.Min(pageSize, items.Count - startIndex);

            return items.GetRange(startIndex, count);
        }
    }
}
