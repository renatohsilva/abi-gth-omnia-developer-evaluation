using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.ViewModels;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleViewModel>>> GetAllSales()
        {
            var query = new GetAllSalesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleViewModel>> GetSaleById(Guid id)
        {
            var query = new GetSaleByIdQuery(id);
            var result = await _mediator.Send(query);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSale([FromBody] CreateSaleRequest request)
        {
            var command = _mapper.Map<CreateSaleCommand>(request);
            var saleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSaleById), new { id = saleId }, saleId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
        {
            var command = _mapper.Map<UpdateSaleCommand>(request);
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}/cancel")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var command = new CancelSaleCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}/items/{itemId}/cancel")]
        public async Task<IActionResult> CancelSaleItem(Guid id, Guid itemId)
        {
            var command = new CancelSaleItemCommand(id, itemId);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
