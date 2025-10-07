using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.CreateSale;

// TheoryData for the Happy Path
public class ValidCreateSaleCommandData : TheoryData<CreateSaleCommand>
{
    public ValidCreateSaleCommandData()
    {
        Add(new CreateSaleCommand
        {
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<CreateSaleItemCommand>
            {
                new() { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10, Discount = 0.1m }
            }
        });
    }
}
