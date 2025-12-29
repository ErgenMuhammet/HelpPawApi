using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetUserAdvertisements
{
    public class GetUserAdvertisementsQueryRequest : IRequest<GetUserAdvertisementsQueryResponse>
    {
        [JsonIgnore]
        public string EmailFromToken { get; set; }
    }
}