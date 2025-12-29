using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.RefreshMyPassword
{
    public class RefreshMyPasswordCommandRequest :IRequest<RefreshMyPasswordCommandResponse>
    {
        [JsonIgnore]
        public string EmailFromToken { get; set; }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; } 
    }
}
