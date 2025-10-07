using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.UpdateSale;

public class InvalidUpdateSaleCommandData : TheoryData<UpdateSaleCommand>
{
    public InvalidUpdateSaleCommandData()
    {
        var validId = Guid.NewGuid();

        // Scenario: Empty Customer Name
        Add(new UpdateSaleCommand
        {
            Id = validId,
            CustomerName = "",
            BranchName = "Valid Branch",
            Items = new List<UpdateSaleItemCommand> { new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 10, Discount = 0 } }
        });

        // Scenario: No items in the sale
        Add(new UpdateSaleCommand
        {
            Id = validId,
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<UpdateSaleItemCommand>()
        });

        // Scenario: Item with zero quantity
        Add(new UpdateSaleCommand
        {
            Id = validId,
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<UpdateSaleItemCommand> { new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 0, UnitPrice = 10, Discount = 0 } }
        });

        // Scenario: Item with invalid discount (quantity < 4 but discount > 0)
        Add(new UpdateSaleCommand
        {
            Id = validId,
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<UpdateSaleItemCommand> { new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 10, Discount = 0.1m } }
        });

        // Scenario: Item with discount greater than 1
        Add(new UpdateSaleCommand
        {
            Id = validId,
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<UpdateSaleItemCommand> { new() { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 10, Discount = 1.1m } }
        });
    }
}
