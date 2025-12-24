using HelpPawApi.Application.Interfaces;
using HelpPawApi.ChatHub;
using Microsoft.AspNetCore.SignalR;


namespace HelpPaw.Infrustructure.ChatService
{
    public class ChatServices : IChatService
    {
        private readonly IHubContext<SignalRHub> _chatService;
        public async Task SendToAllAsync(string user, string message)
        {
            await _chatService.Clients.All.SendAsync("ReceiveMessage",user, message);
        }

        public async Task SendToUserAsync(string FromUser, string ToUser, string message)
        {
            await _chatService.Clients.Group(ToUser).SendAsync("ReceivePrivateMessage", ToUser, message);
        }
    }
}
