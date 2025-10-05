namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;

public class GetSaleByIdResult
{
    public Guid Id { get; set; }
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalValue { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
    public List<SaleItemResult> Items { get; set; } = [];
}

public class SaleItemResult
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
