using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;
    private readonly IMapper _mapper;
    private readonly DiscountSpecification _discountSpecification;

    public UpdateSaleHandler(ISaleRepository saleRepository, IPublisher publisher, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _publisher = publisher;
        _mapper = mapper;
        _discountSpecification = new DiscountSpecification();
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id);
        
        if (sale == null)
            throw new NotFoundException(nameof(Sale), command.Id);

        sale.SaleNumber = command.SaleNumber;
        sale.CustomerName = command.CustomerName;
        sale.BranchName = command.BranchName;

        var itemsToRemove = sale.Items
           .Where(i => !command.Items.Any(dto => dto.Id == i.Id))
           .ToList();

        foreach (var item in itemsToRemove)
        {
            sale.Items.Remove(item);
        }

        foreach (var itemDto in command.Items)
        {
            var existingItem = sale.Items.FirstOrDefault(i => i.Id == itemDto.Id);

            if (existingItem != null)
            {
                existingItem.ProductId = itemDto.ProductId;
                existingItem.Quantity = itemDto.Quantity;
                existingItem.UnitPrice = itemDto.UnitPrice;

                existingItem.Discount = _discountSpecification.GetDiscount(existingItem);
                existingItem.TotalValue = existingItem.Quantity * existingItem.UnitPrice * (1 - existingItem.Discount);
            }
        }

        sale.TotalValue = sale.Items.Sum(i => i.TotalValue);

        var updatedSale = await _saleRepository.UpdateAsync(sale);

        await _publisher.Publish(new SaleModifiedEvent(sale), cancellationToken);

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }
}
