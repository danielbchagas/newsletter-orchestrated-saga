using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.CreateSale;

public record CreateSaleNotification(SaleTransport Sale) : INotification;