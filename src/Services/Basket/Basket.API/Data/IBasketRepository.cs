namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShopingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
        Task<ShopingCart> StoreBasketAsync(ShopingCart basket, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    }
}
