using HelpPaw.Persistence.Configuration;
using HelpPaw.Persistence.Context;
using HelpPawApi.Application.DTOs.Query.GetAdvertisement;
using HelpPawApi.Application.DTOs.Query.GetAllAdvertisement;
using HelpPawApi.Application.DTOs.Query.GetAllAdvertisementWithoutLocation;
using HelpPawApi.Application.DTOs.Query.GetNearestAdvertisement;
using HelpPawApi.Application.DTOs.Query.GetUserAdvertisements;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAdvertisementsController : ControllerBase
    {
        private readonly IMediator _mediatR;
        private readonly UserManager<AppUsers> _userManager;
        private readonly IdentityContext _identityContext;
        private readonly IAppContext _context;

        public GetAdvertisementsController(UserManager<AppUsers> userManager, IdentityContext identityContext, IMediator mediatR, IAppContext context)
        {
            _identityContext = identityContext;
            _userManager = userManager;
            _mediatR = mediatR;
            _context = context;
        }

        [Authorize]
        [HttpGet("User/Get/All")]
        public async Task<IActionResult> GetAllAdvertisements()
        {
            var UserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            if (UserEmail == null)
            {
                UserEmail = User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value;
            }

            if (UserEmail == null)
            {
                UserEmail = User.Identity.Name;
            }
            if (string.IsNullOrEmpty(UserEmail))
            {
                return BadRequest("Token Geçerli Fakat kullanıcı şehir bilgisine ulaşılamadı.");
            }

            var request = new GetAllAdvertisementQueryRequest();
          
            request.EmailFromToken = UserEmail;

            var result = await _mediatR.Send(request);
            
            if (!result.IsSucces)
            {
                var request2= new GetAllAdvertisementWithoutLocationRequest();
                
                var result2 = await _mediatR.Send(request2);
                if (!result2.IsSucces)
                {
                    return BadRequest("İlanlar yuklenirken bir sorun oldu.");
                }
                return Ok(result2);
            }
            return Ok(result);
            
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetAdvertisement([FromRoute] string id)
        {
            var advs = new GetAdvertisementQueryRequest
            {
                AdvertisementId = id
            };


            var result = await _mediatR.Send(advs);

            if (result == null)
            {
                return NotFound("İlan bilgileri yüklenemedi.");
            }

            if (advs != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("User/Get/PastAdvertisement")]
        public async Task<IActionResult> GetUserAdvertisements()
        {          
            var Emailromtoken = User.FindFirst(ClaimTypes.Email)?.Value;// Token içinden Kullanıcı ID'sini alıyoruz 


            if (string.IsNullOrEmpty(Emailromtoken))// Eğer NameIdentifier boşsa, alternatif olarak Name claim'ini kontrol edelim
            {
                Emailromtoken = User.FindFirst(ClaimTypes.Name)?.Value;
            }

            // Hala bulamadıysak hata dönelim
            if (string.IsNullOrEmpty(Emailromtoken))
            {
                return Unauthorized("Token geçerli ancak içinde Kullanıcı ID bilgisi (NameIdentifier) bulunamadı.");
            }


            var query = new GetUserAdvertisementsQueryRequest
            {
                EmailFromToken = Emailromtoken
            };

            var result = await _mediatR.Send(query);

            if (result.IsSucces)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [Authorize]
        [HttpGet("Vet/Get/All")]
        public async Task<IActionResult> GetNearestAdvertisements()
        {
            var Emailromtoken = User.FindFirst(ClaimTypes.Email)?.Value;// Token içinden Kullanıcı ID'sini alıyoruz 


            if (string.IsNullOrEmpty(Emailromtoken))// Eğer NameIdentifier boşsa, alternatif olarak Name claim'ini kontrol edelim
            {
                Emailromtoken = User.FindFirst(ClaimTypes.Name)?.Value;
            }

            // Hala bulamadıysak hata dönelim
            if (string.IsNullOrEmpty(Emailromtoken))
            {
                return Unauthorized("Token geçerli ancak içinde Kullanıcı ID bilgisi (NameIdentifier) bulunamadı.");
            }


            var request = new GetNearestAdvertisementQueryRequest
            {
                EmailFromToken = Emailromtoken
            };

            var result = await _mediatR.Send(request);

            if (!result.IsSucces)
            {
                var request2 = new GetAllAdvertisementWithoutLocationRequest();       
                
                var result2 = await _mediatR.Send(request2);

                if (!result2.IsSucces)
                {
                    return BadRequest(result2);
                }

                return Ok(result2);
            }
             
            
            return Ok(result);

        }
    }
}
