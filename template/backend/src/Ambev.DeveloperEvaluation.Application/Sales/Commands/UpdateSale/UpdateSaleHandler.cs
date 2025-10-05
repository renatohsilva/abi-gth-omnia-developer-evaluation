using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(ISaleRepository saleRepository, IPublisher publisher, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _publisher = publisher;
        _mapper = mapper;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Sale), request.Id);
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

            var existingItem = sale.Items.FirstOrDefault(i => i.Id == itemDto.Id);
            if (existingItem != null)
            {
                existingItem.ProductId = itemDto.ProductId;
                existingItem.Quantity = itemDto.Quantity;
                existingItem.UnitPrice = itemDto.UnitPrice;
                existingItem.Discount = discount;
                existingItem.TotalValue = itemTotal;
            }
        }

        sale.TotalValue = sale.Items.Sum(i => i.TotalValue);

        var updatedSale = await _saleRepository.UpdateAsync(sale);

        await _publisher.Publish(new SaleModifiedEvent(sale), cancellationToken);

        var result = _mapper.Map<UpdateSaleResult>(updatedSale);

        return result;
    }
}
