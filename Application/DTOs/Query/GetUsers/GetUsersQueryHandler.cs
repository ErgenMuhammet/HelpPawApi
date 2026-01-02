using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var Users = await _userManager.Users.Select(x => new UserListDto { Id = x.Id, FullName = x.FullName , Email = x.Email}).ToListAsync(cancellationToken);

            if (Users == null)
            {
                return new GetUsersQueryResponse
                {
                    IsSucces = false,
                    Message = "Kullanıcı Listesi Görüntülenemedi",
                    Users = null
                };
            }

            if (!Users.Any())
            {
                return new GetUsersQueryResponse
                {
                    IsSucces = false,
                    Message = "Görüntülenecek Kullanıcı bulunamadı",
                    Users = null
                };
            }

            return new GetUsersQueryResponse
            {
                IsSucces = true,
                Users = Users,
                Message = "Kullanıcı Listesi Getirildi"
            };

        }
    }
}
