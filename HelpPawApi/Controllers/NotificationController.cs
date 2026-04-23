using HelpPawApi.Application.DTOs.Command.notification.CreateNotification;
using HelpPawApi.Application.DTOs.Query.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace HelpPawApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
             var result = await _mediator.Send(new GetMyNotificationsQueryRequest(userId));
            return Ok(result);
        }


        [HttpPost("test")]
        public async Task<IActionResult> CreateTestNotification(
       CreateNotificationCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
