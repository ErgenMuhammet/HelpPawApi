using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.UpdateVet
{
    
    public class UpdateVetCommandHandler : IRequestHandler<UpdateVetCommandRequest, UpdateVetCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public UpdateVetCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<UpdateVetCommandResponse> Handle(UpdateVetCommandRequest request, CancellationToken cancellationToken)
        {
            var Vet =(Vet) await _userManager.FindByEmailAsync(request.EmailFromToken);
          
                if (Vet == null)
                {
                    return new UpdateVetCommandResponse
                    {
                        IsSucces = false,
                        Message = "Kullanıcı bulunamadı."
                    };
                }


            Vet.FullName = request.FullName;
            Vet.City = request.City;
            Vet.PhoneNumber = request.PhoneNumber;
            Vet.BirthDate = request.Bhirtday;
            Vet.Location = request.ClinicLocation;
            Vet.VeterinaryClinicName = request.VeterinaryClinicName;
           
            var result = await _userManager.UpdateAsync(Vet);
            
            if(result.Succeeded)
            {
                return new UpdateVetCommandResponse

                {
                      IsSucces = true,
                      Message = "Bilgiler Başarı İle güncellendi",
                      Errors=null,
                      VetId = Vet.Id

                 };
            }

            return new UpdateVetCommandResponse
            {
                IsSucces = false,
                Message = "Bilgiler güncellenirken hata oluştu",
                VetId = Vet.Id,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        
    }
}
