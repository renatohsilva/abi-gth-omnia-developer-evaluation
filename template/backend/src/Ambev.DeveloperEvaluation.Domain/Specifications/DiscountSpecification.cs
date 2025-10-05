using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class DiscountSpecification : ISpecification<SaleItem>
{
    public bool IsSatisfiedBy(SaleItem item)
    {
        return item.Quantity >= 4; 
    }

    public decimal GetDiscount(SaleItem item)
    {
        if (item.Quantity >= 10)
            return 0.20m;
        if (item.Quantity >= 4)
            return 0.10m;

        return 0m;
    }
}
