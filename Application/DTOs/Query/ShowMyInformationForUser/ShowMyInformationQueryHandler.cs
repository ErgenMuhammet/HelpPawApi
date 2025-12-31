using HelpPawApi.Application.DTOs.Query.ShowMyInformationForUser;
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
    public class ShowMyInformationForVetQueryHandler : IRequestHandler<ShowMyInformationForUserQueryRequest, ShowMyInformationForVetQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public ShowMyInformationForVetQueryHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        public  async Task<ShowMyInformationForVetQueryResponse> Handle(ShowMyInformationForUserQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailFromToken);

            if (user == null)
            {
                return new ShowMyInformationForVetQueryResponse
                {
                    Message = "Kullanıcı bilgileri bulunmadı.",
                    IsSucces = false,
                };
            }

            return new ShowMyInformationForVetQueryResponse
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
