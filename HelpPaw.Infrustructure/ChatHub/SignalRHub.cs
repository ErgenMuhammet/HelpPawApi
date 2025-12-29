using HelpPawApi.Application.DTOs.Command.ChatMessage;
using MediatR;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace HelpPawApi.ChatHub
{
    public class SignalRHub  : Hub
    {
        private readonly IMediator _mediatR;
        private readonly ILogger<SignalRHub> _logger;

        public SignalRHub(IMediator mediatR, ILogger<SignalRHub> logger)
        {
            _mediatR = mediatR;
            _logger = logger;           
        }

        public override async Task OnConnectedAsync()
        {
            var UserName = Context.GetHttpContext().Request.Query["Kimlik"];

            if (!string.IsNullOrEmpty(UserName))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, UserName);
                _logger.LogInformation($"{UserName} sisteme bağlandı");
            }
            else
            {
                _logger.LogInformation($"isimsiz bir kullanıcı sisteme bağlandı : {Context.ConnectionId} ");
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var UserName = Context.GetHttpContext().Request.Query["kimlik"];

            _logger.LogInformation($"{UserName} isimli kullanıcı sistemden ayrıldı");

            await base.OnDisconnectedAsync(exception);
        }
        
        public  async Task SendMessage(string message  , string? ToUser = null)
        {
            var UserName = Context.GetHttpContext().Request.Query["kimlik"];

            await _mediatR.Send(new ChatMessageCommandRequest(UserName, ToUser, message));
        }

        
    }
}
