using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using AutoMapper;
using FluentValidation;
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

    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

         if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = new Sale
        {
            SaleNumber = command.SaleNumber,
            CustomerName = command.CustomerName,
            BranchName = command.BranchName,
            SaleDate = DateTime.UtcNow,
            IsCancelled = false
        };

        foreach (var itemDto in command.Items)
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

        var result = _mapper.Map<CreateSaleResult>(createdSale);

        await _publisher.Publish(new SaleCreatedEvent(sale), cancellationToken);

        return _mapper.Map<CreateSaleResult>(createdSale);
    }
}
