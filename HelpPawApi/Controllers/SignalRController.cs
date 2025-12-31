using HelpPawApi.Application.DTOs.Query.GetUserAdvertisements;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class SignalRController : ControllerBase
    {
        private readonly IMediator _mediatR;
        public SignalRController(IMediator mediatR)
        {
            _mediatR = mediatR;
        }


        [HttpGet("UsersForChat")]
        public async Task<IActionResult> GetUsers()
        {

            var request =  new GetUserAdvertisementsQueryRequest();
            
            var result = await _mediatR.Send(request);

            if (result.IsSucces == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        //[HttpGet("GetPastMessages")]
        //public async Task<IActionResult> GetMessages()
        //{

        //}
        
    }
}
