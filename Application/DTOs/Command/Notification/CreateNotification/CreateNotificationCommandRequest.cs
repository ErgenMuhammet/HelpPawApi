using HelpPawApi.Domain.Entities.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.notification.CreateNotification
{
    public class CreateNotificationCommandRequest
        : IRequest<CreateNotificationCommandResponse>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
