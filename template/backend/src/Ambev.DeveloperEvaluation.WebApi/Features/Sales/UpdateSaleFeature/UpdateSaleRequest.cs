namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

public class UpdateSaleRequest
{
    public int SaleNumber { get; set; } = 0;
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public List<UpdateSaleItemRequest> Items { get; set; } = [];
}

public class UpdateSaleItemRequest
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
