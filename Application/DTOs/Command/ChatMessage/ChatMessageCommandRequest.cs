using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.ChatMessage
{
    public record ChatMessageCommandRequest(string FromUser ,string ToUser,string Message) : IRequest<ChatMessageCommandResponse>
    {
        
        
    }
}
