using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.GetUser
{
    public class GetUserQueryRequest : IRequest<GetUserQueryResponse>
    {
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
