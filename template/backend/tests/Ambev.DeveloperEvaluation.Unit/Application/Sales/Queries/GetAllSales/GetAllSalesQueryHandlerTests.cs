using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetAllSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.Queries.GetAllSales;

public class GetAllSalesQueryHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly GetAllSalesHandler _handler;

    public GetAllSalesQueryHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetAllSalesHandler(_saleRepository, _mapper);
    }

    [Fact(DisplayName = "Given there are sales When getting all sales Then returns all sales")]
    public async Task Handle_WhenSalesExist_ReturnsAllSales()
    {
        // Given
        var query = new GetAllSalesQuery();
        var sales = new List<Sale> { new Sale(), new Sale() };
        var expectedResult = new List<GetAllSalesResult> { new GetAllSalesResult(), new GetAllSalesResult() };

        _saleRepository.GetAllAsync().Returns(sales);
        _mapper.Map<IEnumerable<GetAllSalesResult>>(sales).Returns(expectedResult);

        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        await _saleRepository.Received(1).GetAllAsync();
        _mapper.Received(1).Map<IEnumerable<GetAllSalesResult>>(sales);
    }

    [Fact(DisplayName = "Given there are no sales When getting all sales Then returns empty list")]
    public async Task Handle_WhenNoSalesExist_ReturnsEmptyList()
    {
        // Given
        var query = new GetAllSalesQuery();
        var sales = new List<Sale>();
        var expectedResult = new List<GetAllSalesResult>();

        _saleRepository.GetAllAsync().Returns(sales);
        _mapper.Map<IEnumerable<GetAllSalesResult>>(sales).Returns(expectedResult);

        // When
        var result = await _handler.Handle(query, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Should().BeEmpty();
        await _saleRepository.Received(1).GetAllAsync();
        _mapper.Received(1).Map<IEnumerable<GetAllSalesResult>>(sales);
    }
}
