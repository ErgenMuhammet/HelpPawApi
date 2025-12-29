using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.UpdateUser
{
    
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, UpdateUserCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public UpdateUserCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailFromToken);
          
                if (user == null)
                {
                    return new UpdateUserCommandResponse
                    {
                        IsSucces = false,
                        Message = "Kullanıcı bulunamadı."
                    };
                }
            

            user.FullName = request.FullName;
            user.City = request.City;
            user.PhoneNumber = request.PhoneNumber;           
            user.BirthDate = request.Bhirtday;

          
            if (user is Vet vetUser)
            {
                if (!string.IsNullOrEmpty(request.VeterinaryClinicName))
                    vetUser.VeterinaryClinicName = request.VeterinaryClinicName;

                if (request.ClinicLocation != null)
                {
                    vetUser.Location = request.ClinicLocation;
                }

            }
                var result = await _userManager.UpdateAsync(user);
            
            if(result.Succeeded)
            {
                return new UpdateUserCommandResponse

                {
                      IsSucces = true,
                      Message = "Bilgiler Başarı İle güncellendi",
                      Errors=null,
                      UserId=user.Id

                 };
            }

            return new UpdateUserCommandResponse
            {
                IsSucces = false,
                Message = "Bilgiler güncellenirken hata oluştu",
                UserId = user.Id,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        
    }
}
