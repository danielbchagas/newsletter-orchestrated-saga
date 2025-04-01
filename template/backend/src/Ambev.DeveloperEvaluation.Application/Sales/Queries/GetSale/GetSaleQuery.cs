using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;

public abstract record GetSaleQuery(Guid Id) : IRequest<GetSaleResult>;