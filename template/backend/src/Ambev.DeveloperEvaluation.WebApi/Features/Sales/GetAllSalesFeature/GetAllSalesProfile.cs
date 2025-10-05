using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetAllSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAllSalesFeature;

public class GetAllSalesProfile : Profile
{
    public GetAllSalesProfile()
    {
        CreateMap<GetAllSalesResult, GetAllSalesResponse>();
    }
}
