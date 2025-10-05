using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;
    private readonly IMapper _mapper;
    private readonly DiscountSpecification _discountSpecification;

    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IPublisher publisher,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _publisher = publisher;
        _mapper = mapper;
        _discountSpecification = new DiscountSpecification();
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale
        {
            SaleNumber = request.SaleNumber,
            CustomerName = request.CustomerName,
            BranchName = request.BranchName,
            SaleDate = DateTime.UtcNow,
            IsCancelled = false
        };

        foreach (var itemDto in request.Items)
        {
            var saleItem = new SaleItem
            {
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice,
                IsCancelled = false
            };

            saleItem.Discount = _discountSpecification.GetDiscount(saleItem);
            saleItem.TotalValue = saleItem.Quantity * saleItem.UnitPrice * (1 - saleItem.Discount);

            sale.Items.Add(saleItem);
        }

        sale.TotalValue = sale.Items.Sum(i => i.TotalValue);

        var createdSale = await _saleRepository.CreateAsync(sale);

        await _publisher.Publish(new SaleCreatedEvent(sale), cancellationToken);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
