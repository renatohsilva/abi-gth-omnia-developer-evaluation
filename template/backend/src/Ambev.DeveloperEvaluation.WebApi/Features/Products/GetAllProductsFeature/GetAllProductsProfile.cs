using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProductsFeature;

public class GetAllProductsProfile : Profile
{
    public GetAllProductsProfile()
    {
        CreateMap<GetAllProductsResult, GetAllProductsResponse>();
    }
}
