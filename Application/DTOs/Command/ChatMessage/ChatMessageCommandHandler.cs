using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.ChatMessage
{
    public class ChatMessageCommandHandler : IRequestHandler<ChatMessageCommandRequest, ChatMessageCommandResponse>
    {
        public Task<ChatMessageCommandResponse> Handle(ChatMessageCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
