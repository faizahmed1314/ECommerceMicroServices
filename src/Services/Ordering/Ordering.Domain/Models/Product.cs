namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
