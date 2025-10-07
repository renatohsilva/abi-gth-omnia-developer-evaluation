using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData.UpdateSale;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Commands.UpdateSale;

public class UpdateSaleCommandHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleCommandHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _publisher = Substitute.For<IPublisher>();
        _handler = new UpdateSaleHandler(_saleRepository, _publisher, _mapper);
    }

    [Theory(DisplayName = "Given valid sale data When updating sale Then returns success response")]
    [ClassData(typeof(ValidUpdateSaleCommandData))]
    public async Task Handle_WithValidData_ShouldUpdateSaleSuccessfully(UpdateSaleCommand command)
    {
        // Arrange
        var existingSale = new Sale { Id = command.Id, Items = new List<SaleItem>() };
        _saleRepository.GetByIdAsync(command.Id).Returns(existingSale);
        _mapper.Map<UpdateSaleResult>(Arg.Any<Sale>()).Returns(new UpdateSaleResult { Id = command.Id });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(command.Id);
        await _saleRepository.Received(1).UpdateAsync(Arg.Any<Sale>());
        await _publisher.Received(1).Publish(Arg.Any<INotification>(), Arg.Any<CancellationToken>());
    }

    [Theory(DisplayName = "Given a non-existent sale id When updating sale Then throws NotFoundException")]
    [ClassData(typeof(ValidUpdateSaleCommandData))] // Use valid data to ensure validation passes
    public async Task Handle_WithNonExistentSaleId_ShouldThrowNotFoundException(UpdateSaleCommand command)
    {
        // Arrange
        _saleRepository.GetByIdAsync(command.Id).ReturnsNull();

        // Act
        var action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Theory(DisplayName = "Given invalid sale data When updating sale Then throws ValidationException")]
    [ClassData(typeof(InvalidUpdateSaleCommandData))]
    public async Task Handle_WithInvalidData_ShouldThrowValidationException(UpdateSaleCommand command)
    {
        // Arrange
        // For validation tests, we don't need the repository to return a sale
        var existingSale = new Sale { Id = command.Id };
        _saleRepository.GetByIdAsync(command.Id).Returns(existingSale);

        // Act
        var action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<ValidationException>();
    }
}
