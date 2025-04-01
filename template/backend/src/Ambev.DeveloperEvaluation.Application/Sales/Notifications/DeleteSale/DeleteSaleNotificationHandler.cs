using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.DeleteSale;

public class DeleteSaleNotificationHandler : INotificationHandler<DeleteSaleNotification>
{
    private ILogger<DeleteSaleNotificationHandler> _logger;
    
    public DeleteSaleNotificationHandler(ILogger<DeleteSaleNotificationHandler> logger)
        => _logger = logger;
    
    public Task Handle(DeleteSaleNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sale deleted: {Notification}", notification);

        return Task.CompletedTask;
    }
}