using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPublisher _publisher;

        public CreateSaleHandler(ISaleRepository saleRepository, IPublisher publisher)
        {
            _saleRepository = saleRepository;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                CustomerName = request.CustomerName,
                BranchName = request.BranchName,
                SaleDate = DateTime.UtcNow,
                IsCancelled = false
            };

            decimal totalSaleValue = 0;

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
                var saleItem = new SaleItem
                {
                    ProductId = itemDto.ProductId,
                    ProductName = itemDto.ProductName,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    Discount = discount,
                    TotalValue = itemTotal,
                    IsCancelled = false
                };
                sale.Items.Add(saleItem);
                totalSaleValue += itemTotal;
            }

            sale.TotalValue = totalSaleValue;

            await _saleRepository.AddAsync(sale);

            await _publisher.Publish(new SaleCreatedEvent(sale), cancellationToken);

            return sale.Id;
        }
    }
}
