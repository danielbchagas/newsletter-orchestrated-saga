using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.DeleteSale;

public record DeleteSaleNotification(Guid Id) : INotification;