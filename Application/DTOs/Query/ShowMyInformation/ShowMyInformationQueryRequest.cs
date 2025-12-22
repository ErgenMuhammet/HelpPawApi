using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ShowMyInformation
{
    public class ShowMyInformationQueryRequest : IRequest<ShowMyInformationQueryResponse>
    {
        [JsonIgnore]
        public string? EmailFromToken{ get; set; }
    }
}
