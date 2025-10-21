﻿

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShopingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("User name is required"); ;

        }
    }

    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShopingCart cart = command.Cart;
            // todo: store basket to database
            // todo: update cache

            await repository.StoreBasketAsync(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }
    }
}
