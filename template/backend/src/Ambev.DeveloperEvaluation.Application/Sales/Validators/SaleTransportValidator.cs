using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators;

public class SaleTransportValidator : AbstractValidator<SaleTransport>
{
    public SaleTransportValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 50).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(sale => sale.Date)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("{PropertyName} cannot be in the future.");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(sale => sale.CustomerName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(sale => sale.BranchName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .Length(1, 100).WithMessage("{PropertyName} must be between {MinLength} and {MaxLength} characters.");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("Sale must have at least one item.")
            .Must(items => items.All(item => item.Quantity > 0)).WithMessage("All sale items must have a quantity greater than zero.")
            .Must(items => items.All(item => item.UnitPrice > 0)).WithMessage("All sale items must have a unit price greater than zero.");
    }
}