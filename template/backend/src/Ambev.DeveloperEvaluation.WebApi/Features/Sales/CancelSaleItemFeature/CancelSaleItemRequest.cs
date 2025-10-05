namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItemFeature;

public class CancelSaleItemRequest
{
    public Guid Id { get; set; }
    public Guid ItemId { get; set; }
}
