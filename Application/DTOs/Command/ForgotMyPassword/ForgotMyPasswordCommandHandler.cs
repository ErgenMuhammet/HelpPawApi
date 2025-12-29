using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace HelpPawApi.Application.DTOs.Command.ForgotMyPassword
{
    public class ForgotMyPasswordCommandHandler : IRequestHandler<ForgotMyPasswordCommandRequest, ForgotMyPasswordCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;

        public ForgotMyPasswordCommandHandler(UserManager<AppUsers> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ForgotMyPasswordCommandResponse> Handle(ForgotMyPasswordCommandRequest request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                var response = await _userManager.AddPasswordAsync(user, request.Password);

                if (response.Succeeded)
                {
                    return new ForgotMyPasswordCommandResponse
                    {
                        IsSucces = true,
                        message = "Şifreniz sıfırlanmıştır.Yeni şifreniz ile giriş yapabilirsiniz.",
                        UserId = user.Id,
                        errors = null

                    };
                }
                else
                {
                    return new ForgotMyPasswordCommandResponse
                    {
                        IsSucces = false,
                        message = "Şifreniz sıfırlanırken hata oluştu.Daha sonra tekrar deneyiniz",
                        UserId = user.Id,
                        errors = response.Errors.Select(e => e.Description).ToList()

                    };
                }

            }
            return new ForgotMyPasswordCommandResponse
            {
                IsSucces = false,
                message = "Kullanıcı bulunamadı",
                UserId = user.Id,
                errors = null

            };

        }
    }
}
