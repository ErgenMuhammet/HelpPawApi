using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.RefreshMyPassword
{
    public class RefreshMyPasswordCommandResponse
    {
        public string Message { get; set; }
        public List<string> error { get; set; }
        public bool IsSucces { get; set; }
    }
}
