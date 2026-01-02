using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.SendMessage
{
    public class SendMessageCommandRequest :IRequest<SendMessageCommandResponse>
    {
        [JsonIgnore]
        public string? SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }

    }
}
