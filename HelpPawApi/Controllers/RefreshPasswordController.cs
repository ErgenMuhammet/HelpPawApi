using HelpPawApi.Application.DTOs.Command.ForgotMyPassword;
using HelpPawApi.Application.DTOs.Command.RefreshMyPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshPasswordController:ControllerBase
    {
        private readonly IMediator _mediatR;

        public RefreshPasswordController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ForgotMyPasswordCommandRequest request)
        {
            var response = await _mediatR.Send(request);

            if (!response.IsSucces) // Senin response içindeki bool değişkenin adı
                return BadRequest(response);

            return Ok(response);
        }

            [Authorize]
            [HttpPost("RefreshPassword")]
            public async Task<IActionResult> RefreshPassword([FromBody] RefreshMyPasswordCommandRequest request)
            {
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

                if (!response.IsSucces) // Senin response içindeki bool değişkenin adı
                    return BadRequest(response);

                return Ok(response);
            }

        }
}
