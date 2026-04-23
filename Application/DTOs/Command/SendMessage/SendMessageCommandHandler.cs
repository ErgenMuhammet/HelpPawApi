using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Chat;
using HelpPawApi.Domain.Entities.Notification;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.SendMessage
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommandRequest, SendMessageCommandResponse>
    {
        private readonly IAppContext _appContext;
        private readonly IChatService _chatService;
        private readonly ILogger<SendMessageCommandHandler> _logger;
        private readonly INotificationService _notificationService;
        public SendMessageCommandHandler(IAppContext appContext, IChatService chatService, ILogger<SendMessageCommandHandler> logger, INotificationService notificationService)
        {
            _appContext = appContext;
            _chatService = chatService;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task<SendMessageCommandResponse> Handle(SendMessageCommandRequest request, CancellationToken cancellationToken)
        {
            var newMessage = new ChatMessage
            {
                CreatedTime = DateTime.UtcNow,
                IsDeleted = false,
                IsRead = false,
                ReceiverId = request.ReceiverId,
                SenderId = request.SenderId,
                Message = request.Message,
                Id = Guid.NewGuid()
            };

            await _appContext.Messages.AddAsync(newMessage);

            var result = await _appContext.SaveChangesAsync(cancellationToken);

            

            if (result <= 0)
            {
                return new SendMessageCommandResponse
                {
                    IsSucces = false,
                    Message = "Mesaj veritabanına kaydedilemedi!"
                };

            }
            var notification = new Notification
            {
                UserId = request.ReceiverId,
                Title = "Yeni Mesajınız Var",
                Message = "Yeni bir mesaj aldınız. Sohbet ekranına giderek okuyabilirsiniz.",
                Type = NotificationType.NewMessage,
                IsRead = false
            };

            await _appContext.Notifications.AddAsync(notification, cancellationToken);
            await _appContext.SaveChangesAsync(cancellationToken);

           
            await _notificationService.SendAsync(notification);
            bool socketSuccess = false;

            try
            {
                await _chatService.SendToUserAsync(request.SenderId, request.ReceiverId, request.Message);
                socketSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mesaj DB'ye kaydedildi ama Socket iletilemedi. Hata: {ex.Message}");
                socketSuccess = false;
            }
            return new SendMessageCommandResponse
            {
                Message = "Mesaj Başarı ile gönderildi",
                IsSucces = true,

            };

            string returnMessage = socketSuccess
                ? "Mesaj başarıyla gönderildi."
                : "Mesaj kaydedildi fakat anlık iletimde sorun oluştu (Sayfayı yenileyince görünür).";

            return new SendMessageCommandResponse
            {
                IsSucces = true, 
                Message = returnMessage
            };

           
        }
    }
}
