using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ChatMessageHistory
{
    public class ChatMessageHistoryQueryRequest: IRequest<ChatMessageHistoryQueryResponse>
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }


    }
}
