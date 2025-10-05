using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;        
    private readonly IPublisher _publisher;
    private readonly IMapper _mapper;

    public CreateSaleHandler(ISaleRepository saleRepository, IPublisher publisher, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
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

        var createdSale = await _saleRepository.CreateAsync(sale);

        await _publisher.Publish(new SaleCreatedEvent(sale), cancellationToken);

        var result = _mapper.Map<CreateSaleResult>(createdSale);
        return result;
    }
}
