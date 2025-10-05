using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public UpdateSaleHandler(ISaleRepository saleRepository, IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id);

            if (sale == null)
            {
                throw new NotFoundException(nameof(Sale), request.Id);
            }

            sale.CustomerName = request.CustomerName;
            sale.BranchName = request.BranchName;

            var itemsToRemove = sale.Items.Where(i => !request.Items.Any(dto => dto.Id == i.Id)).ToList();
            foreach (var item in itemsToRemove)
            {
                sale.Items.Remove(item);
            }

            foreach (var itemDto in request.Items)
            {
                decimal discount = 0;
                if (itemDto.Quantity >= 10)
                {
                    discount = 0.20m;
                }
                else if (itemDto.Quantity >= 4)
                {
                    discount = 0.10m;
                }

                var itemTotal = itemDto.Quantity * itemDto.UnitPrice * (1 - discount);

                if (itemDto.Id.HasValue)
                {
                    var existingItem = sale.Items.FirstOrDefault(i => i.Id == itemDto.Id.Value);
                    if (existingItem != null)
                    {
                        existingItem.ProductId = itemDto.ProductId;
                        existingItem.ProductName = itemDto.ProductName;
                        existingItem.Quantity = itemDto.Quantity;
                        existingItem.UnitPrice = itemDto.UnitPrice;
                        existingItem.Discount = discount;
                        existingItem.TotalValue = itemTotal;
                    }
                }
                else
                {
                    var newItem = new SaleItem
                    {
                        ProductId = itemDto.ProductId,
                        ProductName = itemDto.ProductName,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice,
                        Discount = discount,
                        TotalValue = itemTotal,
                        IsCancelled = false
                    };
                    sale.Items.Add(newItem);
                }
            }

            sale.TotalValue = sale.Items.Sum(i => i.TotalValue);

            await _saleRepository.UpdateAsync(sale);

            await _publisher.Publish(new SaleModifiedEvent(sale), cancellationToken);

            return Unit.Value;
        }
    }
}
