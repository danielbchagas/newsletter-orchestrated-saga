using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.UpdateSale;

public record UpdateSaleNotification(SaleTransport Sale) : INotification;