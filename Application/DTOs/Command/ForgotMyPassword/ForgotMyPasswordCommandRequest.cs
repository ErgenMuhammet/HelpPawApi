using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.ForgotMyPassword
{
    public class ForgotMyPasswordCommandRequest :IRequest<ForgotMyPasswordCommandResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
