using HelpPawApi.Application.DTOs.Query.GetUserAdvertisements;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HelpPawApi.Application.DTOs.Query.ChatMessageHistory;
using HelpPawApi.Application.DTOs.Command.SendMessage;
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

            var request = new GetUserAdvertisementsQueryRequest();

            var result = await _mediatR.Send(request);

            if (result.IsSucces == false)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("Get/PastMessages/{TargetId}")]
        public async Task<IActionResult> GetMessages([FromRoute] string TargetId)
        {
            var CurrentId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (CurrentId == null)
            {
                return Unauthorized("Oturum açmanız gerekmektedir.");
            }

            var request = new ChatMessageHistoryQueryRequest
            {
                CurrentUserId = CurrentId.Value,
                TargetUserId = TargetId
            };

            var response = await _mediatR.Send(request);

            if (!response.IsSucces)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpPost("Send/Message")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommandRequest request)
        {
            var SenderId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);


            if (SenderId == null)
            {
                return Unauthorized("Mesaj göndermek için giriş yapmalısınız.");
            }

            request.SenderId = SenderId.Value;

            var response = await _mediatR.Send(request);

            if (!response.IsSucces)
            {
                return BadRequest(response);
            }

            return Ok(response);
        
        }

    }
}
