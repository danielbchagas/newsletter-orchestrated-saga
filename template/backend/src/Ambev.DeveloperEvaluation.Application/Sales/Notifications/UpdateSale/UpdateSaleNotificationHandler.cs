using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.UpdateSale;

public class UpdateSaleNotificationHandler : INotificationHandler<UpdateSaleNotification>
{
    private readonly ILogger<UpdateSaleNotificationHandler> _logger;
    
    public UpdateSaleNotificationHandler(ILogger<UpdateSaleNotificationHandler> logger)
        => _logger = logger;
    
    public Task Handle(UpdateSaleNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sale updated: {Notification}", notification);

        return Task.CompletedTask;
    }
}