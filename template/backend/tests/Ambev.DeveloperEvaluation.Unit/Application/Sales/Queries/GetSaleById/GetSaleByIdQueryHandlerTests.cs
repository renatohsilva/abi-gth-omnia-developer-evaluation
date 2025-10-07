using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Queries.GetSaleById;

public class GetSaleByIdQueryHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetSaleByIdHandler _handler;

    public GetSaleByIdQueryHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetSaleByIdHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given valid sale id When getting sale by id Then returns sale")]
    public async Task Handle_WhenSaleExists_ReturnsSale()
    {
        // Given
        var saleId = Guid.NewGuid();
        var query = new GetSaleByIdQuery(saleId);
        var sale = new Sale { Id = saleId };
        var expectedResult = new GetSaleByIdResult { Id = saleId };

        _saleRepository.GetByIdAsync(saleId).Returns(sale);
        _mapper.Map<GetSaleByIdResult>(sale).Returns(expectedResult);

        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Id.Should().Be(saleId);
        await _saleRepository.Received(1).GetByIdAsync(saleId);
        _mapper.Received(1).Map<GetSaleByIdResult>(sale);
    }

    [Fact(DisplayName = "Given invalid sale id When getting sale by id Then throws KeyNotFoundException")]
    public async Task Handle_WhenSaleDoesNotExist_ThrowsKeyNotFoundException()
    {
        // Given
        var saleId = Guid.NewGuid();
        var query = new GetSaleByIdQuery(saleId);

        _saleRepository.GetByIdAsync(saleId).ReturnsNull();

        // When
        var act = () => _handler.Handle(query, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _saleRepository.Received(1).GetByIdAsync(saleId);
        _mapper.DidNotReceive().Map<GetSaleByIdResult>(Arg.Any<Sale>());
    }
}
