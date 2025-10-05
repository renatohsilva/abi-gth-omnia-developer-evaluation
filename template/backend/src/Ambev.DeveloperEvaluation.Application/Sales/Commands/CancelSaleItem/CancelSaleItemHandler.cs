using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;

public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, Unit>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;

    public CancelSaleItemHandler(ISaleRepository saleRepository, IPublisher publisher)
    {
        _saleRepository = saleRepository;
        _publisher = publisher;
    }

    public async Task<Unit> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleId);

        if (sale == null)
            throw new NotFoundException(nameof(sale), request.SaleId);

        var itemToCancel = sale.Items.FirstOrDefault(i => i.Id == request.SaleItemId);

        if (itemToCancel == null)
            throw new NotFoundException("SaleItem", request.SaleItemId);

        itemToCancel.IsCancelled = true;

        sale.TotalValue = sale.Items.Where(i => !i.IsCancelled).Sum(i => i.TotalValue);

        await _saleRepository.UpdateAsync(sale);

        await _publisher.Publish(new ItemCancelledEvent(sale.Id, itemToCancel.Id), cancellationToken);

        return Unit.Value;
    }
}
