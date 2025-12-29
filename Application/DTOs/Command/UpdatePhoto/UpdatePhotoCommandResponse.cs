using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.UpdatePhoto
{
    public class UpdatePhotoCommandResponse
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsSucces { get; set; }
    }
}

