namespace BuildingBlocks.Pagination
{
    record class PaginationRequest(int PageNumber = 1, int PageSize = 10);
}
