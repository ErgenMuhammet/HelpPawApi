using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.VetInterestedAdvertisement
{
    public class VetInterestedAdvertisementCommandRequest : IRequest<VetInterestedAdvertisementCommandResponse>
    {
        public string? AdvertisementId { get; set; }
        public string? VetEmailFromToken { get; set; }
    }
}
