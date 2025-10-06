using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id ?? Guid.Empty));

        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        CreateMap<UpdateSaleItemResult, UpdateSaleItemResponse>();
    }
}