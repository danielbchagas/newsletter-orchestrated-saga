using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;

public record DeleteSaleCommand(Guid Id) : IRequest<DeleteSaleResult>;