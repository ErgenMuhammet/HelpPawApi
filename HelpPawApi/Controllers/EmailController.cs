using HelpPawApi.Application.DTOs.Command.ConfirmEmail;
using HelpPawApi.Application.DTOs.Command.ForgotMyPassword;
using HelpPawApi.Application.DTOs.Command.PasswordRefreshMail;
using HelpPawApi.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediatR;

        private readonly IEmailServices _services;

        public EmailController(IMediator mediatR, IEmailServices services)
        {
            _mediatR = mediatR;
            _services = services;
        }

      

        [HttpPost("AccountRecovery")]
        public async Task<IActionResult> ConfirmCode([FromBody] PasswordRefreshMailCommandRequest request)
        {
            var response = await _mediatR.Send(request);

            if (!response.IsSucces) 
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userid, string token)
        {
            var request = new ConfirmEmailCommandRequest
            {
                UserId = userid,
                Token = token,
            };
            var response = await _mediatR.Send(request);

            if (response.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(response.Message);
        }
    }
}
