using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool Success);
    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be null.");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName cannot be empty.");
            RuleFor(x => x.BasketCheckoutDto.TotalPrice).GreaterThan(0).WithMessage("TotalPrice must be greater than 0.");
            // Add more validation rules as needed
        }
    }
    public class CheckoutBasketCommandHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            // Get existing basket with total price

            var basket = await basketRepository.GetBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }
            // Set total price on BasketCheckout event message

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            // Send basket checkout event to RabbitMQ using MassTransit
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            // delete the basket
            await basketRepository.DeleteBasketAsync(command.BasketCheckoutDto.UserName, cancellationToken);
            return new CheckoutBasketResult(true);
        }
    }
}
