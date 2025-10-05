namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByIdFeature;

public class GetSaleByIdResponse
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; } = 0;
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalValue { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public List<SaleItemResponse> Items { get; set; } = [];
}

public class SaleItemResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalValue { get; set; }
    public bool IsCancelled { get; set; }
}
