namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByIdFeature;

public class GetProductByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Sku { get; set; } = string.Empty;
}
