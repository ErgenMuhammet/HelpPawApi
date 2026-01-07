using HelpPawApi.Application.DTOs.Query.GetUser;
using HelpPawApi.Application.DTOs.Query.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class GetUsersController : ControllerBase
    {
        private readonly IMediator _mediator;
 
        public GetUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpGet("Get/Users")]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var response = await _mediator.Send(new GetUsersQueryRequest());

        //    if (!response.IsSucces)
        //    {
        //        return BadRequest(response);
        //    }

        //    return Ok(response);
        //}

        [Authorize]
        [HttpGet("Get/User")]
        public async Task<IActionResult> GetUser()
        {
            
            var CurrentId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (CurrentId == null)
            {
                return Unauthorized("oturum açmanız gerekmektedir");
            }
            var request =  new GetUserQueryRequest();

            request.CurrentUserId = CurrentId.Value;

            var response = await _mediator.Send(request);
            

            if (!response.IsSucces)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }


}
