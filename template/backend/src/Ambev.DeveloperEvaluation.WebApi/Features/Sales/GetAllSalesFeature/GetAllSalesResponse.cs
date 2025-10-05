namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSalesFeature;

public class GetAllSalesResponse
{
    public Guid Id { get; set; }
    public int SaleNumber { get; set; } = 0;
    public DateTime SaleDate { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalValue { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public bool IsCancelled { get; set; }
}
