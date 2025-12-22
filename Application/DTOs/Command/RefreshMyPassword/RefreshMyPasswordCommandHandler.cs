using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.RefreshMyPassword
{
    public class RefreshMyPasswordCommandHandler : IRequestHandler<RefreshMyPasswordCommandRequest, RefreshMyPasswordCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        public RefreshMyPasswordCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RefreshMyPasswordCommandResponse> Handle(RefreshMyPasswordCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailFromToken);

            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (result.Succeeded)
                {
                    return new RefreshMyPasswordCommandResponse
                    {
                        error = result.Errors.Select(e => e.Description).ToList(),
                        IsSucces = true,
                        Message = "Şifreniz yenilendi.Bir sonraki girişte yeni şifrenizi kullanabilirsiniz. "
                    };
                }
                else
                {
                    return new RefreshMyPasswordCommandResponse
                    {
                        error = result.Errors.Select(e => e.Description).ToList(),
                        IsSucces = false,
                        Message = "Şifreniz yenilenirken bir hata oluştu."
                    };
                }
            }

            return new RefreshMyPasswordCommandResponse
            {
                error = null,
                IsSucces = false,
                Message = "Kullanıcı bulunamadı."
            };


        }
    }
}
