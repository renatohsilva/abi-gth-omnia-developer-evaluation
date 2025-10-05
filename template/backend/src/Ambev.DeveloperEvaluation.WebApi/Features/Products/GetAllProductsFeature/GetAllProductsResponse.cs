namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProductsFeature;

public class GetAllProductsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
}
