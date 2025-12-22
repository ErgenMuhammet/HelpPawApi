using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.CreateVet
{
    public class CreateVetCommandResponse
    {
        public bool IsSucces { get; set; }
        public string Messages { get; set; }
        public IList<string> Errors { get; set; }
        public string UsersId { get; set; }
    }
}
