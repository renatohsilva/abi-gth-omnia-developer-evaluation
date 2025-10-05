namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models
{
    public class UpdateSaleRequest
    {
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public List<UpdateSaleItemRequest> Items { get; set; } = new List<UpdateSaleItemRequest>();
    }

    public class UpdateSaleItemRequest
    {
        public Guid? Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
