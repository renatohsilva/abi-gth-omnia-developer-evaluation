namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models
{
    public class CreateSaleRequest
    {
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemRequest> Items { get; set; } = new List<SaleItemRequest>();
    }

    public class SaleItemRequest
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
