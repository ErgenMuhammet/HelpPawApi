using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.ConfirmEmail
{
    
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommandRequest, ConfirmEmailCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public ConfirmEmailCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ConfirmEmailCommandResponse> Handle(ConfirmEmailCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return new ConfirmEmailCommandResponse
                {
                    IsSuccess = false,  
                    Message = "Kullanıcı bulunamadı."
                };
            }
                var decodedTokenBytes = WebEncoders.Base64UrlDecode(request.Token);
                var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (result.Succeeded)
            {
                return new ConfirmEmailCommandResponse
                {
                    IsSuccess = true,
                    Message = "Email adresi başarıyla doğrulandı. Artık giriş yapabilirsiniz."
                };
            }

            return new ConfirmEmailCommandResponse
            {
                IsSuccess = false,
                Message = "Doğrulama başarısız. Link geçersiz veya süresi dolmuş."
            };



        }
    }
}
