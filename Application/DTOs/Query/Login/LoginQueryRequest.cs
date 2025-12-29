using HelpPawApi.Application.MediatR.Query.Login;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace HelpPawApi.Application.DTOs.Query.Login
{
    public class LoginQueryRequest : IRequest<LoginQueryResponse>
    {
        
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
