using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;

public abstract record DeleteSaleCommand(Guid Id) : IRequest<DeleteSaleResult>;