using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.SendMessage
{
    public class SendMessageCommandResponse
    {       
        public bool IsSucces { get; set; }
        public string Message { get; set; }
    }
}
