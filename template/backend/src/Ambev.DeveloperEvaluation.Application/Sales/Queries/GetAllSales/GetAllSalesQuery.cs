using Ambev.DeveloperEvaluation.Application.Sales.ViewModels;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetAllSales
{
    public class GetAllSalesQuery : IRequest<IEnumerable<SaleViewModel>>
    {
    }
}
