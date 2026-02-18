namespace ECommerce.Shared.BasketDtos
{
    public record BasketDtos(
        string Id,
        ICollection<BasketItemDtos> Items,
        int? DeliveryMethodId,
         string? PaymentIntentId,
         string? ClientSecret,
         decimal? ShippingCost);

}
