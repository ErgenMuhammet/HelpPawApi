 using HelpPawApi.Application.DTOs.Command.ConfirmEmail;
using HelpPawApi.Application.DTOs.Command.CreateUser;
using HelpPawApi.Application.DTOs.Command.CreateVet;
using HelpPawApi.Application.DTOs.Command.ForgotMyPassword;
using HelpPawApi.Application.DTOs.Command.PasswordRefreshMail;
using HelpPawApi.Application.DTOs.Command.UpdateUser;
using HelpPawApi.Application.DTOs.Command.CreateAdvertisement;
using HelpPawApi.Application.DTOs.Query.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpPawApi.Controllers
{
        [ApiController]
        [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public AuthController(IMediator mediatR)
        {
            _mediatR = mediatR; 
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]  LoginQueryRequest request)
        {
            var response = await _mediatR.Send(request);

            return Ok(response);

        }

            
    }
}



