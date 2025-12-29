using HelpPawApi.Application.DTOs.Command.CreateVet;
using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppRole;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace HelpPawApi.Application.DTOs.Command.CreateUser
{
    public class CreateVetCommandHandler : IRequestHandler<CreateVetCommandRequest, CreateVetCommandResponse>
    {
        private readonly UserManager<AppUsers> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailServices _emailServices;

        public CreateVetCommandHandler(UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailServices = emailServices;
        }

        public async Task<CreateVetCommandResponse> Handle(CreateVetCommandRequest request, CancellationToken cancellationToken)
        {
            var Vet = new Vet
            {
                FullName = request.FullName,
                Location = request.ClinicLocation, 
                BirthDate = request.BirthDate,
                City = request.City,
                Email = request.Email,

                
                UserName = request.Email,

                VeterinaryClinicName = request.VeterinaryClinicName,
                VeterinerRegistiryNumber = request.VeterinerRegistiryNumber,
                PhoneNumber = request.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var result = await _userManager.CreateAsync(Vet, request.Password);

           
            if (!result.Succeeded)
            {
                
                return new CreateVetCommandResponse
                {
                    IsSucces = false,
                    Messages = "Kayıt işlemi başarısız oldu.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

           

            
            string role = "Veteriner";
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new AppRole { Name = role });
            }
            await _userManager.AddToRoleAsync(Vet, role);

            
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(Vet);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                string baseUrl = "http://localhost:5233";
                var confirmationLink = $"{baseUrl}/api/Auth/ConfirmEmail?userId={Vet.Id}&token={encodedToken}";

                await _emailServices.SendEmailAsync(
                   Vet.Email,
                   "HelpPaw Hesap Doğrulama",
                   $"<h3>Hoşgeldiniz {Vet.FullName}!</h3><p>Hesabınızı doğrulamak ve giriş yapabilmek için lütfen <a href='{confirmationLink}'>buraya tıklayın</a>.</p>"
                );
            }
            catch (Exception ex)
            {
                
                // Loglama yapılabilir.
            }

            return new CreateVetCommandResponse
            {
                IsSucces = true,
                Messages = "Veteriner başarıyla oluşturuldu. Lütfen e-postanızı kontrol edin.",
                UsersId = Vet.Id,
                Errors = null
            };
        }
    }
}


