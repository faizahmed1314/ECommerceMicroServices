namespace BuildingBlocks.Pagination
{
    public record class PaginationRequest(int PageIndex = 1, int PageSize = 10);
}
