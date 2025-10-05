using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleByIdFeature;

public class GetSaleByIdProfile : Profile
{
    public GetSaleByIdProfile()
    {
        CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
        CreateMap<SaleItemResult, SaleItemResponse>();
    }
}
