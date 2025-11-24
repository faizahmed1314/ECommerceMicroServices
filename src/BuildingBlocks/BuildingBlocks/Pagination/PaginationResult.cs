namespace BuildingBlocks.Pagination
{
    public class PaginationResult<TEntity>(int pageIndex, int pageSize, long totalRecords, IEnumerable<TEntity> records) where TEntity : class
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long TotalRecords { get; } = totalRecords;
        public IEnumerable<TEntity> Records { get; } = records;
    }
}
