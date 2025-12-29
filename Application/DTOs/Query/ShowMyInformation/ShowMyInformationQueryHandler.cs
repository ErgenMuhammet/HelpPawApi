using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ShowMyInformation
{
    public class ShowMyInformationQueryHandler : IRequestHandler<ShowMyInformationQueryRequest, ShowMyInformationQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public ShowMyInformationQueryHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        public  async Task<ShowMyInformationQueryResponse> Handle(ShowMyInformationQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailFromToken);

            if (user == null)
            {
                return new ShowMyInformationQueryResponse
                {
                    Message = "Kullanıcı bilgileri bulunmadı.",
                    IsSucces = false,
                };
            }

            return new ShowMyInformationQueryResponse
            {
                BirthDate = user.BirthDate,
                City = user.City,
                IsSucces = true,
                Email = user.Email,
                FullName = user.FullName,
                PhotoUrl = user.PhotoUrl,
                PhoneNumber = user.PhoneNumber
            };
            
        }
    }
}
