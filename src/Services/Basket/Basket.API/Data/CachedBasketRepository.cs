
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {


        public async Task<ShopingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return System.Text.Json.JsonSerializer.Deserialize<ShopingCart>(cachedBasket);
            }

            var basket = await repository.GetBasketAsync(userName, cancellationToken);

            await cache.SetStringAsync(userName, System.Text.Json.JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<ShopingCart> StoreBasketAsync(ShopingCart basket, CancellationToken cancellationToken = default)
        {
            await repository.StoreBasketAsync(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, System.Text.Json.JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            await repository.DeleteBasketAsync(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);

            return true;
        }
    }
}
