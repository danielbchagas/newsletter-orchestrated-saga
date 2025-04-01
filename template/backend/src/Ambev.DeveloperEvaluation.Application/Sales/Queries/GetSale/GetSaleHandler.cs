using Ambev.DeveloperEvaluation.Application.Sales.Notifications.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
{
    private readonly ILogger<GetSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="saleRepository">The sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="mediator">The mediator instance.</param>
    public GetSaleHandler(ILogger<GetSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Handles the retrieval of a sale.
    /// </summary>
    /// <param name="request">The get sale query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the get sale operation.</returns>
    public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetAsync(s => s.Id == request.Id, cancellationToken);
        
        if (sale == null)
        {
            _logger.LogWarning("Sale with ID: {SaleId} not found", request.Id);
            return new GetSaleResult(null);
        }
        
        await _mediator.Publish(new GetSaleNotification(request.Id), cancellationToken);

        return new GetSaleResult(_mapper.Map<SaleTransport>(sale));
    }
}