using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.PasswordRefreshMail
{
    public class PasswordRefreshMailCommandResponse
    {
        public string ConfirmCode { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public bool IsSucces { get; set; }

    }
}
