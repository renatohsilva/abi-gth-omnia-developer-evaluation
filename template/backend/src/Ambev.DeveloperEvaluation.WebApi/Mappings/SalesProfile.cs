using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.ViewModels;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings
{
    public class SalesProfile : Profile
    {
        public SalesProfile()
        {
            // Entity to ViewModel
            CreateMap<Sale, SaleViewModel>();
            CreateMap<SaleItem, SaleItemViewModel>();

            // API Request to Application Command
            CreateMap<CreateSaleRequest, CreateSaleCommand>();
            CreateMap<SaleItemRequest, SaleItemDto>();

            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleItemRequest, SaleItemDto>();
        }
    }
}
