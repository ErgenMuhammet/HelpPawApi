using HelpPawApi.Application.DTOs.Command.ForgotMyPassword;
using HelpPawApi.Application.DTOs.Command.RefreshMyPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class RefreshPasswordController:ControllerBase
    {
        private readonly IMediator _mediatR;

        public RefreshPasswordController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotMyPasswordCommandRequest request)
        {
            var response = await _mediatR.Send(request);

            if (!response.IsSucces) 
                return BadRequest(response);

            return Ok(response);
        }

            [Authorize]
            [HttpPatch("RefreshPassword")]
            public async Task<IActionResult> RefreshPassword([FromBody] RefreshMyPasswordCommandRequest request)
            {
            var userIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;


            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            }

   
            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = User.Identity?.Name;
            }

            if (string.IsNullOrEmpty(userIdentifier))
            {
                return Unauthorized("Token geçerli ama kullanıcı bilgisi (Name/Email) okunamadı.");
            }

            request.EmailFromToken = userIdentifier;

            var response = await _mediatR.Send(request);

                if (!response.IsSucces) 
                    return BadRequest(response);

                return Ok(response);
            }

        }
}
