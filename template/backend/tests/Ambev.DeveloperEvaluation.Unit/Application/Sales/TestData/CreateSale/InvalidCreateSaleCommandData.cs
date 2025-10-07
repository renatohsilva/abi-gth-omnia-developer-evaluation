using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.CreateSale;

// TheoryData for various invalid scenarios
public class InvalidCreateSaleCommandData : TheoryData<CreateSaleCommand>
{
    public InvalidCreateSaleCommandData()
    {
        // Scenario: Empty Customer Name
        Add(new CreateSaleCommand
        {
            CustomerName = "",
            BranchName = "Valid Branch",
            Items = new List<CreateSaleItemCommand> { new() { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 10 } }
        });

        // Scenario: No items in the sale
        Add(new CreateSaleCommand
        {
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<CreateSaleItemCommand>()
        });

        // Scenario: Item with zero quantity
        Add(new CreateSaleCommand
        {
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<CreateSaleItemCommand> { new() { ProductId = Guid.NewGuid(), Quantity = 0, UnitPrice = 10 } }
        });

        // Scenario: Item with invalid discount (quantity < 4 but discount > 0)
        Add(new CreateSaleCommand
        {
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<CreateSaleItemCommand> { new() { ProductId = Guid.NewGuid(), Quantity = 3, UnitPrice = 10, Discount = 0.1m } }
        });

        // Scenario: Item with discount greater than 1
        Add(new CreateSaleCommand
        {
            CustomerName = "Valid Customer",
            BranchName = "Valid Branch",
            Items = new List<CreateSaleItemCommand> { new() { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10, Discount = 1.1m } }
        });
    }
}
