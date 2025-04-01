using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.GetSale;

public record GetSaleNotification(Guid Id) : INotification;