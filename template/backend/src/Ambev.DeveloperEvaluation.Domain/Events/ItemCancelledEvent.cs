using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemCancelledEvent : INotification
{
    public Guid SaleId { get; }
    public Guid SaleItemId { get; }

    public ItemCancelledEvent(Guid saleId, Guid saleItemId)
    {
        SaleId = saleId;
        SaleItemId = saleItemId;
    }
}
