using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.notification
{
    public class CreateVetInterestedNotificationCommand : IRequest
    {
        public string UserId { get; set; }

        public CreateVetInterestedNotificationCommand(string userId)
        {
            UserId = userId;
        }
    }
}
