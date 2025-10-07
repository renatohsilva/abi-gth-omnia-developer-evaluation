using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.UpdateSale;

public class ValidUpdateSaleCommandData : TheoryData<UpdateSaleCommand>
{
    public ValidUpdateSaleCommandData()
    {
        Add(new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<UpdateSaleItemCommand>
            {
                new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10, Discount = 0.1m }
            }
        });
    }
}
