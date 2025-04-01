using Ambev.DeveloperEvaluation.Application.Sales.Notifications;
using Ambev.DeveloperEvaluation.Application.Sales.Notifications.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ILogger<DeleteSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public DeleteSaleHandler(ILogger<DeleteSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var id = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
        
        await _mediator.Publish(new DeleteSaleNotification(id), cancellationToken);
        
        return new DeleteSaleResult(id);
    }
}