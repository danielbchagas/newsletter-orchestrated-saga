using Ambev.DeveloperEvaluation.Application.Sales.Notifications.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ILogger<UpdateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="saleRepository">The sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="mediator">The mediator instance.</param>
    public UpdateSaleHandler(ILogger<UpdateSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Handles the update of a sale.
    /// </summary>
    /// <param name="request">The update sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the update sale operation.</returns>
    /// <exception cref="ValidationException">Thrown when validation fails.</exception>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new SaleTransportValidator().ValidateAsync(request.Data, cancellationToken);

        if (!validationResult.IsValid)
        {
            GenericLogError(validationResult);
            throw new ValidationException(validationResult.Errors);
        }
        
        foreach (var item in request.Data.Items)
        {
            validationResult = await new SaleItemTransportValidator().ValidateAsync(item, cancellationToken);

            if (!validationResult.IsValid)
            {
                GenericLogError(validationResult);
                throw new ValidationException(validationResult.Errors);
            }
        }
        
        var sale = _mapper.Map<Sale>(request);
        sale.ApplyDiscount();
        
        validationResult = await new SaleValidator().ValidateAsync(sale, cancellationToken);

        if (!validationResult.IsValid)
        {
            GenericLogError(validationResult);
            throw new ValidationException(validationResult.Errors);
        }
        
        var result = await _saleRepository.UpdateAsync(sale, cancellationToken);
        
        if (result == null)
        {
            _logger.LogError("Sale not found with ID: {SaleId}", request.Data.Id);
            return new UpdateSaleResult(null);
        }
        
        await _mediator.Publish(new UpdateSaleNotification(request.Data), cancellationToken);
        
        return new UpdateSaleResult(sale.Id);
    }
    
    /// <summary>
    /// Logs validation errors.
    /// </summary>
    /// <param name="validationResult">The validation result containing errors.</param>
    private void GenericLogError(ValidationResult validationResult)
        => _logger.LogError("Validation failed: {Errors}", validationResult.Errors);
}