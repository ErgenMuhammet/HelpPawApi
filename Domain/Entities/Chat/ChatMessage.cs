using HelpPawApi.Domain.Common;
using HelpPawApi.Domain.Entities.AppUser;

namespace HelpPawApi.Domain.Entities.Chat
{
    public class ChatMessage : EntityBase
    {
        public string SenderId { get; set; }
        public AppUsers Sender { get; set; }

        public string ReceiverId { get; set; }
        public AppUsers Receiver { get; set; }

        public bool IsRead { get; set; }
        public string Message { get; set; }


    }
}
