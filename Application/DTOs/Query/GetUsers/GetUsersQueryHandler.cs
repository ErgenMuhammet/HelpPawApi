using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace HelpPawApi.Application.DTOs.Query.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, GetUsersQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public GetUsersQueryHandler(UserManager<AppUsers> userManager)
        {
          _userManager = userManager;   
        }

        public async Task<GetUsersQueryResponse> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var Users = await _userManager.Users.ToListAsync<AppUsers>();

            if (Users == null)
            {
                return new GetUsersQueryResponse
                {
                    Message = "Görüntülenecek kullanıcı bulunamadı.",
                    IsSucces = false,
                    Users = null
                };
            }

            return new GetUsersQueryResponse
            {
                IsSucces = true,
                Users = Users,
                Message = "Kullanıcılar başarı ile listelendi"
            };

        
        }

    }
}
