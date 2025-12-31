using HelpPawApi.Application.DTOs.Query.ShowMyInformation;
using HelpPawApi.Application.DTOs.Query.ShowMyInformationForUser;
using HelpPawApi.Application.DTOs.Query.ShowMyInformationForVet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InformationController : ControllerBase
    {
        private readonly IMediator _mediatR;

        public InformationController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }

        [Authorize]
        [HttpGet("MyInformation/User")]
        public async Task<IActionResult> ShowMyInformationForUser()
        {
            var UserIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            if (UserIdentifier == null)
            {
                UserIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            }
            if (UserIdentifier == null)
            {
                UserIdentifier = User.Identity.Name;
            }
            if (string.IsNullOrEmpty(UserIdentifier))
            {
                return Unauthorized("Kullanıcı bilgileri bulunamadı");
            }

            var request = new ShowMyInformationForUserQueryRequest
            {
                EmailFromToken = UserIdentifier
            };
            
           var result = await _mediatR.Send(request);

            if (!result.IsSucces)
            {
                return BadRequest(result);
            }

            return Ok(result);
           
            

        }

        [Authorize]
        [HttpGet("MyInformation/Vet")]
        public async Task<IActionResult> ShowMyInformationForVet()
        {
            var UserIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            if (UserIdentifier == null)
            {
                UserIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            }
            if (UserIdentifier == null)
            {
                UserIdentifier = User.Identity.Name;
            }
            if (string.IsNullOrEmpty(UserIdentifier))
            {
                return Unauthorized("Kullanıcı bilgileri bulunamadı");
            }

            var request = new ShowMyInformationForVetQueryRequest
            {
                EmailFromToken = UserIdentifier
            };

            var result = await _mediatR.Send(request);

            if (!result.IsSucces)
            {
                return BadRequest(result);
            }

            return Ok(result);



        }
    }
}
