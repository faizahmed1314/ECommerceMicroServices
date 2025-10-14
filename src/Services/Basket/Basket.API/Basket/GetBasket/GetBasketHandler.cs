

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShopingCart ShopingCart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            //todo: get basket from database
            //var basket = await _repository.GetBasketAsync(query.UserName);
            return new GetBasketResult(new ShopingCart(query.UserName));
        }
    }
}
