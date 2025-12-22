using HelpPawApi.Application.DTOs.Command.CreateAdvertisement;
using HelpPawApi.Application.DTOs.Command.CreateUser;
using HelpPawApi.Application.DTOs.Command.CreateVet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class CreateController : ControllerBase
    {
        
        private readonly IMediator _mediatR;

        public CreateController(IMediator _Mediator)
        {
            _mediatR = _Mediator;
        }
        [HttpPost("Register/User")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
        {
            var response = await _mediatR.Send(request);

            if (!response.IsSucces) // Senin response içindeki bool değişkenin adı
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("Register/Vet")]
        public async Task<IActionResult> CreateVet([FromBody] CreateVetCommandRequest request)
        {
            var response = await _mediatR.Send(request);

            if (!response.IsSucces) // Senin response içindeki bool değişkenin adı
                return BadRequest(response);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("Create/Advertisements")]
        public async Task<IActionResult> CreateAdvertisements([FromBody] CreateAdvertisementsCommandRequest request)
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

            if (!response.IsSucces)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


    }
}
