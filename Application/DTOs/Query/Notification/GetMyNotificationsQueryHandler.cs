using HelpPawApi.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.Notification
{
    public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQueryRequest, List<GetMyNotificationsQueryResponse>>
    {
        private readonly IAppContext _context;

        public GetMyNotificationsQueryHandler(IAppContext context)
        {
            _context = context;
        }

        public async Task<List<GetMyNotificationsQueryResponse>> Handle(GetMyNotificationsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _context.Notifications
                .Where(N => N.UserId == request.UserId)
                .OrderByDescending(N => N.CreatedTime)
                .Select(N => new GetMyNotificationsQueryResponse
                 {
                     Id = N.Id,
                     Title = N.Title,
                     Message = N.Message,
                     Type = (int)N.Type,
                    IsRead = N.IsRead,
                     CreatedTime = N.CreatedTime
                 }).ToListAsync(cancellationToken);

        }
    }
}
