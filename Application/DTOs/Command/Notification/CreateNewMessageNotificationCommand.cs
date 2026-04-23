using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.notification
{
    public class CreateNewMessageNotificationCommand :IRequest
    {
        public string UserId { get; set; }
        public CreateNewMessageNotificationCommand(string userId)
        {
            UserId = userId;
        }
    }
}
