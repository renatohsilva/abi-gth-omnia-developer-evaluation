using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Commands.CancelSaleItem;

public class CancelSaleItemCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;
    private readonly CancelSaleItemHandler _handler;

    public CancelSaleItemCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _publisher = Substitute.For<IPublisher>();
        _handler = new CancelSaleItemHandler(_saleRepository, _publisher);
    }

    [Fact(DisplayName = "Given valid sale and item id When cancelling sale item Then returns success")]
    public async Task Handle_ValidSaleAndItemId_ReturnsSuccess()
    {
        // Given
        var saleId = Guid.NewGuid();
        var saleItemId = Guid.NewGuid();
        var command = new CancelSaleItemCommand(saleId, saleItemId);
        var sale = new Sale { Id = saleId, Items = new List<SaleItem> { new SaleItem { Id = saleItemId, TotalValue = 100 } } };

        _saleRepository.GetByIdAsync(saleId).Returns(sale);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().Be(MediatR.Unit.Value);
        sale.Items.First().IsCancelled.Should().BeTrue();
        sale.TotalValue.Should().Be(0);
        await _saleRepository.Received(1).UpdateAsync(sale);
        await _publisher.Received(1).Publish(Arg.Any<INotification>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale id When cancelling sale item Then throws not found exception")]
    public async Task Handle_InvalidSaleId_ThrowsNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var saleItemId = Guid.NewGuid();
        var command = new CancelSaleItemCommand(saleId, saleItemId);
        _saleRepository.GetByIdAsync(saleId).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Given invalid sale item id When cancelling sale item Then throws not found exception")]
    public async Task Handle_InvalidSaleItemId_ThrowsNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var saleItemId = Guid.NewGuid();
        var command = new CancelSaleItemCommand(saleId, saleItemId);
        var sale = new Sale { Id = saleId, Items = new List<SaleItem>() };

        _saleRepository.GetByIdAsync(saleId).Returns(sale);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
