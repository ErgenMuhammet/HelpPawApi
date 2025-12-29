using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.UpdatePhoto
{
    public class UpdatePhotoCommandRequest :IRequest<UpdatePhotoCommandResponse>
    {
        [JsonIgnore]
        public string? EmailFromToken { get; set; }
        public string NewPhoto{ get; set; }
    }
}
