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

    public UpdateSaleHandler(ILogger<UpdateSaleHandler> logger, ISaleRepository saleRepository, IMapper mapper)
    {
        _logger = logger;
        _saleRepository = saleRepository;
        _mapper = mapper;
    }
    
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
        
        _logger.LogInformation("Sale created with ID: {SaleId}", sale.Id);
        
        return new UpdateSaleResult(sale.Id);
    }
    
    private void GenericLogError(ValidationResult validationResult)
        => _logger.LogError("Validation failed: {Errors}", validationResult.Errors);
}