using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetNearestAdvertisement
{
    public class GetNearestAdvertisementQueryRequest : IRequest<GetNearestAdvertisementQueryResponse>
    {
        [JsonIgnore]
        public string EmailFromToken { get; set; }
    }
}
