using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;

public class CancelSaleItemCommand : IRequest<Unit>
{
    public Guid SaleId { get; set; }
    public Guid SaleItemId { get; set; }

    public CancelSaleItemCommand(Guid saleId, Guid saleItemId)
    {
        SaleId = saleId;
        SaleItemId = saleItemId;
    }
}
