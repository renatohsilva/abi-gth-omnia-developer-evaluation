namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleFeature;

public sealed class CreateSaleResponse
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public int SaleNumber { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalValue { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public List<CreateSaleItemResponse> Items { get; set; } = [];
}

public class CreateSaleItemResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalValue { get; set; }
}

