using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();

        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? Guid.Empty));

        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
}