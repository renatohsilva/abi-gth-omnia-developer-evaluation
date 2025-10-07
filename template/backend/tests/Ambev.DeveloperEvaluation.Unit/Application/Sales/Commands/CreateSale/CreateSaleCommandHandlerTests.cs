using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.CreateSale;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Commands.CreateSale;

public class CreateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly CreateSaleHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _publisher = Substitute.For<IPublisher>();
        _handler = new CreateSaleHandler(_saleRepository, _publisher, _mapper);
    }

    [Theory(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    [ClassData(typeof(ValidCreateSaleCommandData))]
    public async Task Handle_WithValidData_ShouldCreateSaleSuccessfully(CreateSaleCommand command)
    {
        // Arrange
        var sale = new Sale { Id = Guid.NewGuid() };
        var saleResult = new CreateSaleResult { Id = sale.Id };

        _mapper.Map<Sale>(command).Returns(sale);
        _saleRepository.CreateAsync(Arg.Any<Sale>()).Returns(sale);
        _mapper.Map<CreateSaleResult>(Arg.Any<Sale>()).Returns(saleResult); // <-- FIX

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(sale.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>());
        await _publisher.Received(1).Publish(Arg.Any<INotification>(), Arg.Any<CancellationToken>());
    }

    [Theory(DisplayName = "Given invalid sale data When creating sale Then throws ValidationException")]
    [ClassData(typeof(InvalidCreateSaleCommandData))]
    public async Task Handle_WithInvalidData_ShouldThrowValidationException(CreateSaleCommand command)
    {
        // Arrange & Act
        var action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
    }
}
