using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, GetProductByIdResult>();
        CreateMap<Product, GetAllProductsResult>();
    }
}
