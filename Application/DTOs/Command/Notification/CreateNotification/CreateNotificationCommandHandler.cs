using HelpPawApi.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpPawApi.Domain.Entities.Notification;

namespace HelpPawApi.Application.DTOs.Command.notification.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommandRequest, CreateNotificationCommandResponse>
    {
        private readonly IAppContext _context;
       private readonly INotificationService _notificationService;


        public CreateNotificationCommandHandler(IAppContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<CreateNotificationCommandResponse> Handle(
            CreateNotificationCommandRequest request,
            CancellationToken cancellationToken)
        {
           var notification = new Notification
           {
                UserId = request.UserId,
                Title = request.Title,
                Message = request.Message,
                Type = request.Type,
                IsRead = false
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _notificationService.SendAsync(notification);

            return new CreateNotificationCommandResponse
            {
                IsSuccess = true
            };
        }
    }

}
