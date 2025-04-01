using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.GetSale;

public class GetSaleNotificationHandler : INotificationHandler<GetSaleNotification>
{
    private readonly ILogger<GetSaleNotificationHandler> _logger;

    public GetSaleNotificationHandler(ILogger<GetSaleNotificationHandler> logger)
        => _logger = logger;
    
    public Task Handle(GetSaleNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sale searched: {Notification}", notification);

        return Task.CompletedTask;
    }
}