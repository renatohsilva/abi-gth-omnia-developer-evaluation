using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Commands.CancelSale;

public class CancelSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPublisher _publisher;
    private readonly CancelSaleHandler _handler;

    public CancelSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _publisher = Substitute.For<IPublisher>();
        _handler = new CancelSaleHandler(_saleRepository, _publisher);
    }

    [Fact(DisplayName = "Given valid sale id When cancelling sale Then returns success")]
    public async Task Handle_ValidSaleId_ReturnsSuccess()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId);
        var sale = new Sale { Id = saleId, Items = new List<SaleItem> { new SaleItem() } };

        _saleRepository.GetByIdAsync(saleId).Returns(sale);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().Be(MediatR.Unit.Value);
        sale.IsCancelled.Should().BeTrue();
        sale.Items.First().IsCancelled.Should().BeTrue();
        await _saleRepository.Received(1).UpdateAsync(sale);
        await _publisher.Received(1).Publish(Arg.Any<INotification>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale id When cancelling sale Then throws not found exception")]
    public async Task Handle_InvalidSaleId_ThrowsNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var command = new CancelSaleCommand(saleId);
        _saleRepository.GetByIdAsync(saleId).ReturnsNull();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
