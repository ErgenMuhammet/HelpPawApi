using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.ForgotMyPassword
{
    public class ForgotMyPasswordCommandResponse
    {
        public string message  { get; set; }
        public string UserId { get; set; }
        public List<string> errors { get; set; }
        public bool IsSucces { get; set; }  

    }
}
