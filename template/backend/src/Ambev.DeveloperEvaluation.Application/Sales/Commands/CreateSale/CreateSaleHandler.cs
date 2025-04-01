using Ambev.DeveloperEvaluation.Application.Sales.Notifications.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ILogger<CreateSaleHandler> _logger;
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateSaleHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="saleRepository">The sale repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="mediator">The mediator instance.</param>
    public CreateSaleHandler(ILogger<CreateSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the creation of a sale.
    /// </summary>
    /// <param name="request">The create sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the create sale operation.</returns>
    /// <exception cref="ValidationException">Thrown when validation fails.</exception>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
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
        
        var exists = await _saleRepository.GetAsync(s => s.Id == sale.Id);
        
        if (exists != null)
        {
            _logger.LogError("Sale with ID {SaleId} already exists", sale.Id);
            throw new ValidationException(new[] { new ValidationFailure(nameof(sale.Id), "Sale already exists") });
        }
        
        await _saleRepository.CreateAsync(sale, cancellationToken);

        await _mediator.Publish(new CreateSaleNotification(request.Data), cancellationToken);
        
        return new CreateSaleResult(sale.Id);
    }

    /// <summary>
    /// Logs validation errors.
    /// </summary>
    /// <param name="validationResult">The validation result containing errors.</param>
    private void GenericLogError(ValidationResult validationResult)
        => _logger.LogError("Validation failed: {Errors}", validationResult.Errors);
}