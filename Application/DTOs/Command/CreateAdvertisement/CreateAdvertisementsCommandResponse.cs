using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.CreateAdvertisement
{
    public class CreateAdvertisementsCommandResponse
    {
        public string UserId { get; set; }
        public bool IsSucces { get; set; }
        public Guid AdvertisementId { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
