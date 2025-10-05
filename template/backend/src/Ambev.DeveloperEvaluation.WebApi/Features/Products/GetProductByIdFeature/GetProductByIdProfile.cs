using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByIdFeature;

public class GetProductByIdProfile : Profile
{
    public GetProductByIdProfile()
    {
        CreateMap<GetProductByIdResult, GetProductByIdResponse>();
    }
}
