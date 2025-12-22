using HelpPawApi.Application.DTOs.Command.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
            // 1. Önce ClaimTypes.Name'e bak (TokenService'de bunu doldurdun)
            var userIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            // 2. Bulamazsa ClaimTypes.Email'e bak
            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            }

            // 3. Hala yoksa User.Identity.Name'e bak
            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = User.Identity?.Name;
            }

            if (string.IsNullOrEmpty(userIdentifier))
            {
                return Unauthorized("Token geçerli ama kullanıcı bilgisi (Name/Email) okunamadı.");
            }

            // TokenService'de userName veya email atadığın için buraya email olarak gönderiyoruz
            request.EmailFromToken = userIdentifier;

            var response = await _mediatR.Send(request);

            if (!response.IsSucces)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
