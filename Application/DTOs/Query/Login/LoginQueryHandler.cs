using HelpPawApi.Application.DTOs.Query.Login;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Application.MediatR.Query.Login;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Query.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQueryRequest, LoginQueryResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly ITokenServices _tokenServices;

        public LoginQueryHandler(UserManager<AppUsers> userManager, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _tokenServices = tokenServices;

        }

        public async Task<LoginQueryResponse> Handle(LoginQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new AuthenticationException("Kullanıcı bulunamadı veya şifre hatalı.");
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!checkPassword)
            {
                throw new AuthenticationException("Kullanıcı bulunamadı veya şifre hatalı.");
            }
            
            if (!user.EmailConfirmed)
            {
                throw new AuthenticationException("Giriş yapabilmek için lütfen önce email adresinize gönderilen linke tıklayarak hesabınızı doğrulayın.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "";

            var token =  _tokenServices.CreateToken(user);

                return new LoginQueryResponse
                {
                    Name = user.FullName,                    
                    Role = userRole,
                    Expiration = DateTime.Now.AddMinutes(45),
                    Token = token,
                };
            
        }
    }
}

