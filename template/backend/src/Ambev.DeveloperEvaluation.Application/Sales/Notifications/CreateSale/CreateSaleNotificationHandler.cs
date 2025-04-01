using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Notifications.CreateSale;

public class CreateSaleNotificationHandler : INotificationHandler<CreateSaleNotification>
{
    private ILogger<CreateSaleNotificationHandler> _logger;
    
    public CreateSaleNotificationHandler(ILogger<CreateSaleNotificationHandler> logger)
        => _logger = logger;
    
    public Task Handle(CreateSaleNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Sale created: {Notification}", notification);

        return Task.CompletedTask;
    }
}