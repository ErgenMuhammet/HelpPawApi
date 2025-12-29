using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Notification;
using HelpPaw.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HelpPaw.Infrastructure
{
    public class NotificationService : INotificationService
    {
        private readonly IAppContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(
            IAppContext context,
            IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task SendVetInterestedAsync(string userId)
        {
            await Send(
                userId,
                "İlanınıza İlgi Var",
                "Bir veteriner ilanınızla ilgilendi.",
                NotificationType.VetInterested
            );
        }

        public async Task SendNewMessageAsync(string userId)
        {
            await Send(
                userId,
                "Yeni Mesaj",
                "Yeni bir mesajınız var.",
                NotificationType.NewMessage
            );
        }

        private async Task Send(
            string userId,
            string title,
            string message,
            NotificationType type)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type
            };

            // DB
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync(CancellationToken.None);

            // SignalR
            await _hubContext
                .Clients
                .Group(userId)
                .SendAsync("ReceiveNotification", new
                {
                    notification.Title,
                    notification.Message,
                    notification.Type,
                    notification.CreatedTime
                });
        }
    }
}
