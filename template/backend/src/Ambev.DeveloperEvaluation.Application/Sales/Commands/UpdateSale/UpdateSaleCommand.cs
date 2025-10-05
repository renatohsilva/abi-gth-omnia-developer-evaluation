using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
    }
}
