using HelpPawApi.Application.DTOs.Command.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Identity.Client;
using HelpPawApi.Application.DTOs.Command.UpdatePhoto;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController:ControllerBase
    {
        private readonly IMediator _mediatR;

        public UpdateController(IMediator mediator)
        {
            _mediatR = mediator;
        }

        [Authorize]
        [HttpPost("Update/AppUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommandRequest request)
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

        [Authorize]
        [HttpPost("Update/Photo")]
        public async Task<IActionResult> UpdatePhoto([FromBody] UpdatePhotoCommandRequest request)
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

            var result = await _mediatR.Send(request);
            
            if (result.IsSucces)
            {
                return Ok(result);
            }
             
            return BadRequest(result);
        }

    }
}
