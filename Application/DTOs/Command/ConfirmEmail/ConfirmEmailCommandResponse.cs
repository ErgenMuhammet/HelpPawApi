using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.ConfirmEmail
{
    public class ConfirmEmailCommandResponse 
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
