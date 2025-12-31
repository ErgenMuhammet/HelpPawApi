using HelpPawApi.Application.DTOs.Query.ShowMyInformationForUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ShowMyInformation
{
    public class ShowMyInformationForUserQueryRequest : IRequest<ShowMyInformationForVetQueryResponse>
    {
        [JsonIgnore]
        public string? EmailFromToken{ get; set; }
    }
}
