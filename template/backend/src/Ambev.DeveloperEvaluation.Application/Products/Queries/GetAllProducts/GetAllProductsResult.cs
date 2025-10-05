namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProducts;

public class GetAllProductsResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
}
