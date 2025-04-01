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

    public GetSaleHandler(ILogger<GetSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
    }
    
    public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetAsync(s => s.Id == request.Id, cancellationToken);
        
        if (sale == null)
        {
            _logger.LogWarning("Sale with ID: {SaleId} not found", request.Id);
            return new GetSaleResult(null);
        }

        return new GetSaleResult(_mapper.Map<SaleTransport>(sale));
    }
}