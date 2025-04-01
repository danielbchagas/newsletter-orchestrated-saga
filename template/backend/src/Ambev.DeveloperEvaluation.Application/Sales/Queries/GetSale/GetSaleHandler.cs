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

    public GetSaleHandler(ILogger<GetSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    
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