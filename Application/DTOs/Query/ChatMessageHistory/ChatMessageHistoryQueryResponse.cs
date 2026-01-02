using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ChatMessageHistory
{
    
    public class ChatMessageHistoryQueryResponse
    {
        public List<ChatMessageDto>? Messages { get; set; }
        public bool IsSucces { get; set; }
        public string Message{ get; set; }

    }

    public class ChatMessageDto
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public bool IsRead { get; set; }
        public bool IsMine { get; set; }
        public DateTime CreatedDate{ get; set; }
    }
}
    