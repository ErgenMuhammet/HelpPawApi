using HelpPawApi.Domain.Entities.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetUserAdvertisements
{
    public class GetUserAdvertisementsQueryResponse
    {
        public List<Advertisements> PastAdvertisements{ get; set; }
        public bool IsSucces { get; set; }
        public string Message{ get; set; }

       


    }

}