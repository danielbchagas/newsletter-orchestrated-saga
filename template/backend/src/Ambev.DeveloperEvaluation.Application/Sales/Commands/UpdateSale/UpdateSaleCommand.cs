using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public record UpdateSaleCommand(SaleTransport Data) : IRequest<UpdateSaleResult>;