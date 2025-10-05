using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public CancelSaleHandler(ISaleRepository saleRepository, IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task<Unit> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);

            if (sale == null)
            {
                throw new NotFoundException(nameof(sale), request.Id);
            }

            sale.IsCancelled = true;            
            foreach (var item in sale.Items)
            {
                item.IsCancelled = true;
            }

            await _saleRepository.UpdateAsync(sale);

            await _publisher.Publish(new SaleCancelledEvent(sale.Id), cancellationToken);

            return Unit.Value;
        }
    }
}
