using Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.Queries.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductByIdFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProductsFeature;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetAllProductsResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var query = new GetAllProductsQuery();
        var response = await _mediator.Send(query, cancellationToken);

        return Ok(new ApiResponseWithData<IEnumerable<GetAllProductsResponse>>
        {
            Success = true,
            Message = "Products retrieved successfully",
            Data = _mapper.Map<IEnumerable<GetAllProductsResponse>>(response)
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductByIdResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest { Id = id };
        var validator = new GetProductByIdRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var query = new GetProductByIdQuery(request.Id);
        var response = await _mediator.Send(query, cancellationToken);

        if (response == null)
            return NotFound(new ApiResponse { Success = false, Message = "Product not found" });

        return Ok(new ApiResponseWithData<GetProductByIdResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductByIdResponse>(response)
        });
    }
}
