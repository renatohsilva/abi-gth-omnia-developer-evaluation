namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;

public class GetProductByIdResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
}
