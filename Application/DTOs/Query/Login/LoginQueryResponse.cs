using HelpPawApi.Domain.Entities.AppRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.MediatR.Query.Login 
{
    public class LoginQueryResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Name { get; set; }      
        public string Role { get; set; }

    }
}
