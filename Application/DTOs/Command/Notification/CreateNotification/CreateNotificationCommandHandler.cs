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

        public CreateNotificationCommandHandler(IAppContext context)
        {
            _context = context;
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

            return new CreateNotificationCommandResponse
            {
                IsSuccess = true
            };
        }
    }

}
