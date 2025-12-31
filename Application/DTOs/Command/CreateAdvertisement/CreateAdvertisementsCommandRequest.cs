using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Locations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.CreateAdvertisement
{
    public class CreateAdvertisementsCommandRequest : IRequest<CreateAdvertisementsCommandResponse>
    {
        [JsonIgnore]
        public string? EmailFromToken{ get; set; }
        
        public string Title { get; set; }  
        public string AddressDescription { get; set; }  
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Location Location { get; set; }


    }
}
