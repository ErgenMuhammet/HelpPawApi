using HelpPawApi.Application.DTOs.Query.GetAdvertisement;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetAllAdvertisement
{
    public class GetAllAdvertisementQueryRequest : IRequest<GetAllAdvertisementQueryResponse>
    {
        [JsonIgnore]
        public string EmailFromToken { get; set; }
    }
}
