using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.UpdateSale;

public static class UpdateSaleCommandHandlerTestData
{
    private static readonly Faker<UpdateSaleItemCommand> updateSaleItemCommandFaker = new Faker<UpdateSaleItemCommand>()
        .RuleFor(i => i.Id, f => f.Random.Guid())
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 100))
        .RuleFor(i => i.Discount, f => f.Random.Decimal(0, 10));

    private static readonly Faker<UpdateSaleCommand> updateSaleCommandFaker = new Faker<UpdateSaleCommand>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.SaleNumber, f => f.Random.Int(1, 1000))
        .RuleFor(c => c.CustomerName, f => f.Person.FullName)
        .RuleFor(c => c.BranchName, f => f.Company.CompanyName())
        .RuleFor(c => c.Items, f => updateSaleItemCommandFaker.Generate(f.Random.Int(1, 5)));

    public static UpdateSaleCommand GenerateValidCommand()
    {
        return updateSaleCommandFaker.Generate();
    }
}
