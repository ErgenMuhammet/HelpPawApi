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

        public GetUserQueryHandler(UserManager<AppUsers> userManager)
        {
          _userManager = userManager;   
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            var User = await _userManager.Users.Select(x => new UserDto { Id = x.Id, FullName = x.FullName, Email = x.Email}).FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (User == null)
            {
                return new GetUserQueryResponse
                {
                    Message = "Görüntülenecek kullanıcı bulunamadı.",
                    IsSucces = false,
                    User = null
                };
            }

            return new GetUserQueryResponse
            {
                IsSucces = true,
                User = User,
                Message = "Kullanıcı başarı ile görüntülendi"
            };

        
        }

    }
}
