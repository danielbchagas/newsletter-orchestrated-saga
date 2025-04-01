using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public abstract record UpdateSaleCommand(SaleTransport Data) : IRequest<UpdateSaleResult>;