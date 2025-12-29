using HelpPawApi.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HelpPawApi.Application.DTOs.Command.notification
{
    public class NotificationCommandHandler : IRequestHandler<CreateNewMessageNotificationCommand>, IRequestHandler<CreateVetInterestedNotificationCommand>
    {
        private readonly INotificationService _notificationService;
        public NotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(CreateVetInterestedNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.SendVetInterestedAsync(request.UserId);
        }

        public async Task Handle(CreateNewMessageNotificationCommand request, CancellationToken cancellationToken)
        {
            await _notificationService.SendNewMessageAsync(request.UserId);
        }
    }
}
