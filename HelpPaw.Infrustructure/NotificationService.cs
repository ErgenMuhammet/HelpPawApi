using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Notification;
using HelpPaw.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HelpPaw.Infrastructure
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // TEK VE GERÇEK METOT
        public async Task SendAsync(Notification notification)
        {
            await _hubContext
                .Clients
                .User(notification.UserId) // Identity UserId
                .SendAsync("ReceiveNotification", new
                {
                    notification.Id,
                    notification.Title,
                    notification.Message,
                    notification.Type,
                    notification.CreatedTime
                });
        }
    }
}
