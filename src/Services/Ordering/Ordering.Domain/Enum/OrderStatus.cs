namespace Ordering.Domain.Enum
{
    public enum OrderStatus
    {
        Draft = 1,
        Pending = 2,
        Completed = 3,
        //Processing,
        //Shipped,
        //Delivered,
        Cancelled = 4,
        //Returned
    }
}
