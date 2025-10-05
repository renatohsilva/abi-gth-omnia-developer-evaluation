namespace Ambev.DeveloperEvaluation.Application.Sales.ViewModels
{
    public class SaleViewModel
    {
        public Guid Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalValue { get; set; }
        public string BranchName { get; set; }
        public bool IsCancelled { get; set; }
        public List<SaleItemViewModel> Items { get; set; } = new List<SaleItemViewModel>();
    }

    public class SaleItemViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalValue { get; set; }
        public bool IsCancelled { get; set; }
    }
}
