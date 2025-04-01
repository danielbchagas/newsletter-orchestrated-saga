using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public record CreateSaleCommand(SaleTransport Data) : IRequest<CreateSaleResult>;