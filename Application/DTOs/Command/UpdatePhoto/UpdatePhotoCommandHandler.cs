using HelpPawApi.Application.DTOs.Command.UpdateUser;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.UpdatePhoto
{
    public class UpdatePhotoCommandHandler : IRequestHandler<UpdatePhotoCommandRequest, UpdatePhotoCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public UpdatePhotoCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UpdatePhotoCommandResponse> Handle(UpdatePhotoCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailFromToken);

            if (user == null)
            {
                return new UpdatePhotoCommandResponse
                {
                    IsSucces = false,
                    Message = "Token okunamadı."
                };
            }

            user.PhotoUrl = request.NewPhoto;

            return new UpdatePhotoCommandResponse
            {
                IsSucces = true,
                Message = "Profil fotoğrafı başarıyla değştririldi",
                UserId = user.Id
            };

        }
    }
}
