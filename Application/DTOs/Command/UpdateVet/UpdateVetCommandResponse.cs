using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.UpdateVet
{
    public class UpdateVetCommandResponse
    {
        public string VetId { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public bool IsSucces { get; set; }
    }
}
