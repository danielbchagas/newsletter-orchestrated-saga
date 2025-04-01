using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

[ApiController]
[Route("api/[controller]")]
public class SaleController : BaseController
{
    private readonly IMediator _mediator;

    public SaleController(IMediator mediator)
        => _mediator = mediator;
    
    /// <summary>
    /// Get a sale by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSaleQuery(id);
        var result = _mediator.Send(query, cancellationToken).Result;
        return Ok(result);
    }
    
    /// <summary>
    /// Create a new sale
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post([FromBody] SaleTransport request, CancellationToken cancellationToken)
    {
        var validation = new SaleTransportValidator().ValidateAsync(request, cancellationToken).Result;
        
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));
        
        var command = new CreateSaleCommand(request);
        var result = _mediator.Send(command, cancellationToken).Result;
        return Ok(result);
    }
    
    /// <summary>
    /// Update a sale
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] Guid id, [FromBody] SaleTransport request, CancellationToken cancellationToken)
    {
        if(id.CompareTo(request.Id) != 0)
            return BadRequest("Id in query string and body must be the same");
        
        var validation = new SaleTransportValidator().ValidateAsync(request, cancellationToken).Result;
        
        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));
        
        var command = new UpdateSaleCommand(request);
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Delete a sale by ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult Delete([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteSaleCommand(id);
        var result = _mediator.Send(command, cancellationToken).Result;
        return Ok(result);
    }
}