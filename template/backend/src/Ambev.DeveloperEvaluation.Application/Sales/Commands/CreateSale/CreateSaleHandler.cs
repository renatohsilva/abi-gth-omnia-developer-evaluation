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
                var itemTotal = itemDto.Quantity * itemDto.UnitPrice * (1 - itemDto.Discount);
                var saleItem = new SaleItem
                {
                    ProductId = itemDto.ProductId,
                    ProductName = itemDto.ProductName,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice,
                    Discount = itemDto.Discount,
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
