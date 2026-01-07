using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.Advertisement;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace HelpPawApi.Application.DTOs.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQueryRequest, GetUserQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly IAppContext _appContext;

        public GetUserQueryHandler(UserManager<AppUsers> userManager, IAppContext appContext)
        {
          _userManager = userManager;   
          _appContext = appContext;
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            var myMessages = await _appContext.Messages
                .Where(x => x.SenderId == request.CurrentUserId || x.ReceiverId == request.CurrentUserId)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync(cancellationToken);

            if (!myMessages.Any())
            {
                return new GetUserQueryResponse { IsSucces = true, User = new List<UserDto>(), Message = "Mesaj yok." };
            }

            var uniqueUserIds = myMessages
             .Select(m => m.SenderId == request.CurrentUserId ? m.ReceiverId : m.SenderId)
                .Distinct()
                    .ToList();

            var users = await _userManager.Users
                .Where(u => uniqueUserIds.Contains(u.Id))
                     .ToListAsync(cancellationToken);


            var userDtoList = users.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email 
            }). ToList();

            return new GetUserQueryResponse
            {
                IsSucces = true,
                User = userDtoList,
                Message = "Kullanıcılar getirildi."
            };

        }

    }
}
