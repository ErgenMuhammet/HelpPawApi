using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.PasswordRefreshMail
{
    public class PasswordRefreshMailCommandRequest:IRequest<PasswordRefreshMailCommandResponse>
    {
        public string Email { get; set; }
    }
}
