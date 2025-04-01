using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class SaleItemTransportValidator : AbstractValidator<SaleItemTransport>
{
    public SaleItemTransportValidator()
    {
        RuleFor(saleItem => saleItem.ProductId)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .Length(1, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(saleItem => saleItem.ProductName)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty.")
            .Length(1, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(saleItem => saleItem.Quantity)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(saleItem => saleItem.UnitPrice)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisonValue}.");

        RuleFor(saleItem => saleItem.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.")
            .LessThanOrEqualTo(saleItem => saleItem.UnitPrice * saleItem.Quantity).WithMessage("{PropertyName} cannot be greater than the total price.");
    }
}