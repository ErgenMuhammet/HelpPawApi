using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.Notification
{
    public class GetMyNotificationsQueryRequest : IRequest<List<GetMyNotificationsQueryResponse>>
    {
        public string UserId { get; set; }
        public GetMyNotificationsQueryRequest(string userId)
        {
            UserId = userId;
        }
    }
}
