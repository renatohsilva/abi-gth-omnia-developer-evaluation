using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetAllSales;

public class GetAllSalesQuery : IRequest<IEnumerable<GetAllSalesResult>>
{
}
