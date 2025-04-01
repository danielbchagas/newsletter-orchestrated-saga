using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;

public record GetSaleQuery(Guid Id) : IRequest<GetSaleResult>;