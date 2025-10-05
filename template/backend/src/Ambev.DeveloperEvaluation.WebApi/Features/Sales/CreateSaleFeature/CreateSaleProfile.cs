using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleFeature;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<SaleItemRequest, CreateSaleItemCommand>();
    }
}