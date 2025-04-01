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

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="saleRepository">The sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="mediator">The mediator instance.</param>
    public DeleteSaleHandler(ILogger<DeleteSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the deletion of a sale.
    /// </summary>
    /// <param name="request">The delete sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the delete sale operation.</returns>
    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var id = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
        
        await _mediator.Publish(new DeleteSaleNotification(id), cancellationToken);
        
        return new DeleteSaleResult(id);
    }
}