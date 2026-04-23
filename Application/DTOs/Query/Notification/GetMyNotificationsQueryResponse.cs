using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.Notification
{
    public class GetMyNotificationsQueryResponse 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
