using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = new Product();
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalValue { get; set; }
    public bool IsCancelled { get; set; }

    public Guid SaleId { get; set; }
    public virtual Sale Sale { get; set; } = new Sale();
}
