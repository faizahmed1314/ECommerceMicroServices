namespace Basket.API.Models
{
    public class ShopingCart
    {
        public string UserName { get; set; } = string.Empty;
        public List<ShopingCartItem> Items { get; set; } = new();
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }

        public ShopingCart(string userName)
        {
            UserName = userName;
        }


        // Required for mapping
        public ShopingCart()
        {
        }
    }
}
