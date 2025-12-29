using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetAllAdvertisementWithoutLocation
{
    public class GetAllAdvertisementWithoutLocationResponse
    {
        public List<Advertisements> Advertisements { get; set; }
        public string Message { get; set; }
        public bool IsSucces { get; set; }
    }
}
