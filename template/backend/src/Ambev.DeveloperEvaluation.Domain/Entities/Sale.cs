
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public DateTime SaleDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalValue { get; set; }
        public string BranchName { get; set; }
        public bool IsCancelled { get; set; }
        public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}
