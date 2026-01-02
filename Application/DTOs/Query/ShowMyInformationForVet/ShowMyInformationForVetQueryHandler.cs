using HelpPawApi.Application.DTOs.Query.ShowMyInformation;
using HelpPawApi.Application.DTOs.Query.ShowMyInformationForUser;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.ShowMyInformationForVet
{
    public class ShowMyInformationForVetQueryHandler : IRequestHandler<ShowMyInformationForVetQueryRequest, ShowMyInformationForVetQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public ShowMyInformationForVetQueryHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        public  async Task<ShowMyInformationForVetQueryResponse> Handle(ShowMyInformationForVetQueryRequest request, CancellationToken cancellationToken)
        {
            var vet = (Vet) await _userManager.FindByEmailAsync(request.EmailFromToken);

            if (vet == null)
            {
                return new ShowMyInformationForVetQueryResponse
                {
                    Message = "Kullanıcı bilgileri bulunmadı.",
                    IsSucces = false,
                };
            }

            return new ShowMyInformationForVetQueryResponse
            {
                BirthDate = vet.BirthDate,
                City = vet.City,
                IsSucces = true,
                Email = vet.Email,
                FullName = vet.FullName,
                PhotoUrl = vet.PhotoUrl,
                PhoneNumber = vet.PhoneNumber,
                Location = vet.Location,
                VeterinaryClinicName = vet.VeterinaryClinicName,
                VeterinerRegistiryNumber = vet.VeterinerRegistiryNumber,
                Message = "Veteriner Bilgileri getirildi"
            };
            
        }
    }
}
