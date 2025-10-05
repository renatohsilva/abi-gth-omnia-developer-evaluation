namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleFeature;

public class CreateSaleRequest
{
    public int SaleNumber { get; set; } = 0;
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public List<SaleItemRequest> Items { get; set; } = [];
}

public class SaleItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
}
