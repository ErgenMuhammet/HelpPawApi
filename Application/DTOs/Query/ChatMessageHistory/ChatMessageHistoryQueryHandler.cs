using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ChatMessageHistory
{
    public class ChatMessageHistoryQueryHandler : IRequestHandler<ChatMessageHistoryQueryRequest, ChatMessageHistoryQueryResponse>
    {
        private readonly IAppContext _appContext;
        private readonly UserManager<AppUsers> _userManager;

        public ChatMessageHistoryQueryHandler(IAppContext appContext, UserManager<AppUsers> userManager)
        {
            _appContext = appContext;
            _userManager = userManager;
        }
        public async Task<ChatMessageHistoryQueryResponse> Handle(ChatMessageHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            var messages = await _appContext.Messages.Where
                (
                    x => x.SenderId == request.CurrentUserId && x.ReceiverId == request.TargetUserId
                ).OrderBy(z => z.CreatedTime).Select(i => new ChatMessageDto
                {
                    Id = i.Id.ToString(),
                    SenderId = i.SenderId,
                    ReceiverId = i.ReceiverId,
                    MessageContent = i.Message,
                    CreatedDate = i.CreatedTime,
                    IsRead = i.IsRead,
                    IsMine = i.SenderId == request.CurrentUserId
                }).ToListAsync(cancellationToken);

            if (!messages.Any())
            {
                return new ChatMessageHistoryQueryResponse
                {
                    
                    Messages = new List<ChatMessageDto>(),
                    Message = "Henüz bir sohbet geçmişi yok.",
                    IsSucces = true
                };
            }

            return new ChatMessageHistoryQueryResponse
            {
                
                Messages = messages,
                Message = "Sohbet geçmişi getirildi.",
                IsSucces = true
            };

        }
    }
}
