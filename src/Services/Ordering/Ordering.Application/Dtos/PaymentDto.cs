namespace Ordering.Application.Dtos
{
    public record PaymentDto(
        int PaymentMethod,
        string CardNumber,
        string? CardHolderName,
        string Expiration,
        string Cvv
    );

}
