namespace BuildingBlocks.Pagination
{
    public class PaginationResult<TEntity>(int pageNumber, int pageSize, int totalRecords, IEnumerable<TEntity> records) where TEntity : class
    {
        public int PageNumber { get; } = pageNumber;
        public int PageSize { get; } = pageSize;
        public int TotalRecords { get; } = totalRecords;
        public IEnumerable<TEntity> Records { get; } = records;
    }
}
