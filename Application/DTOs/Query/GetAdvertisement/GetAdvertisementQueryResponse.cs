using HelpPawApi.Domain.Entities.AppUser;
using HelpPawApi.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetAdvertisement
{
    public class GetAdvertisementQueryResponse
    {
        public string Title { get; set; }
        public string AddressDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string UserName { get; set; }

        public Location Location { get; set; }

        public bool IsSucces { get; set; }
        public string Message { get; set; } 
    }
}
