using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailServices _emailServices;
        public CreateUserCommandHandler(UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailServices = emailServices;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var User = new User
            {
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    BirthDate = request.BirthDate,
                    UserName = request.Email,
                    City = request.City,

                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
            };   
            
            var result = await _userManager.CreateAsync(User, request.Password);

            if (result.Succeeded)
            {
                string role = "Kullanıcı";
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new AppRole { Name = role });
                }
                await _userManager.AddToRoleAsync(User, role);

                try
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(User);

                    var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                    string baseUrl = "http://localhost:5233";
                    var confirmationLink = $"{baseUrl}/api/Auth/ConfirmEmail?userId={User.Id}&token={encodedToken}";

                    await _emailServices.SendEmailAsync(
                       User.Email,
                       "HelpPaw Hesap Doğrulama",
                       $"<h3>Hoşgeldiniz {User.FullName}!</h3><p>Hesabınızı doğrulamak ve giriş yapabilmek için lütfen <a href='{confirmationLink}'>buraya tıklayın</a>.</p>"
                   );

                }
                catch (Exception ex)
                {
                   
                    return new CreateUserCommandResponse
                    {
                        IsSucces = false,
                        Messages = "Kullanıcı oluşturulurken hata meydana geldi.",
                        Errors = result.Errors.Select(e => e.Description).ToList()
                    };
                }

             
            }
             return new CreateUserCommandResponse
             {
                IsSucces = true,
                Messages = "Kullanıcı Başarı ile olusturuldu.Lütfen giriş yapabilmek için email adresinizdeki doğrulama linkine tıklayınız.",
                Errors = result.Errors.Select(e => e.Description).ToList()
             };


        }
    }
}

