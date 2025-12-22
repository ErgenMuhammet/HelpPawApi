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
                Location = request.ClinicLocation, // Dikkat: Request'teki isimle Entity eşleşmeli
                BirthDate = request.BirthDate,
                City = request.City,
                Email = request.Email,

                // DÜZELTME 1: UserName zorunludur. Genelde Email ile aynı yapılır.
                UserName = request.Email,

                VeterinaryClinicName = request.VeterinaryClinicName,
                VeterinerRegistiryNumber = request.VeterinerRegistiryNumber,
                PhoneNumber = request.PhoneNumber,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var result = await _userManager.CreateAsync(Vet, request.Password);

            // DÜZELTME 2: Önce başarısızlık durumunu kontrol et!
            if (!result.Succeeded)
            {
                // Eğer kayıt başarısızsa (Email kullanılıyor vb.) BURADAN dönmelisin.
                return new CreateVetCommandResponse
                {
                    IsSucces = false,
                    Messages = "Kayıt işlemi başarısız oldu.",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            // --- BURAYA GELDİYSE KAYIT BAŞARILIDIR ---

            // Rol İşlemleri
            string role = "Veteriner";
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new AppRole { Name = role });
            }
            await _userManager.AddToRoleAsync(Vet, role);

            // Email Gönderme İşlemi
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
                // Mail gitmese bile kullanıcı oluştuğu için Success dönüyoruz ama hatayı not edebilirsin.
                // Loglama yapılabilir.
            }

            // DÜZELTME 3: Başarılı dönüşü en sonda yapıyoruz.
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

//using HelpPawApi.Application.DTOs.Command.CreateVet;
//using HelpPawApi.Application.Interfaces;
//using HelpPawApi.Domain.Entities.AppRole;
//using HelpPawApi.Domain.Entities.AppUser;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.WebUtilities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HelpPawApi.Application.DTOs.Command.CreateUser
//{
//    public class CreateVetCommandHandler : IRequestHandler<CreateVetCommandRequest, CreateVetCommandResponse>
//    {
//        private readonly UserManager<AppUsers> _userManager;
//        private readonly RoleManager<AppRole> _roleManager;
//        private readonly IEmailServices _emailServices;
//        public CreateVetCommandHandler(UserManager<AppUsers> userManager, RoleManager<AppRole> roleManager, IEmailServices emailServices)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _emailServices = emailServices;
//        }

//        public async Task<CreateVetCommandResponse> Handle(CreateVetCommandRequest request, CancellationToken cancellationToken)
//        {
//            var Vet = new Vet
//            {
//                FullName = request.FullName,
//                Location = request.ClinicLocation,
//                BirthDate = request.BirthDate,
//                City = request.City,
//                Email = request.Email,
//                VeterinaryClinicName = request.VeterinaryClinicName,
//                VeterinerRegistiryNumber = request.VeterinerRegistiryNumber,
//                PhoneNumber = request.PhoneNumber,
//                CreatedDate = DateTime.UtcNow,
//                IsDeleted = false

//            };

//            var result = await _userManager.CreateAsync(Vet, request.Password);
//            if (result.Succeeded)
//            {
//                string role = "Veteriner";
//                if (!await _roleManager.RoleExistsAsync(role))
//                {
//                    await _roleManager.CreateAsync(new AppRole { Name = role });
//                } 

//                await _userManager.AddToRoleAsync(Vet, role);

//                try
//                {
//                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(Vet);

//                    var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

//                    string baseUrl = "http://localhost:5233";

//                    var confirmationLink = $"{baseUrl}/api/Auth/ConfirmEmail?userId={Vet.Id}&token={encodedToken}";

//                    await _emailServices.SendEmailAsync(
//                       Vet.Email,
//                       "HelpPaw Hesap Doğrulama",
//                       $"<h3>Hoşgeldiniz {Vet.FullName}!</h3><p>Hesabınızı doğrulamak ve giriş yapabilmek için lütfen <a href='{confirmationLink}'>buraya tıklayın</a>.</p>"
//                   );

//                }
//                catch (Exception ex)
//                {
//                    // Mail hatası, kullanıcı oluşturmayı durdurmamalı.
//                    // İleride buraya loglama ekleyebilirsin.
//                    return new CreateVetCommandResponse
//                    {
//                        IsSucces = false,
//                        Messages = "Kullanıcı oluşturulurken hata meydana geldi.",
//                        Errors = result.Errors.Select(e => e.Description).ToList()
//                    };
//                }


//            }
//            return new CreateVetCommandResponse
//            {
//                IsSucces = true,
//                Messages = "Veteriner Başarı ile olusturuldu.",
//                Errors = result.Errors.Select(e => e.Description).ToList()
//            };

//        }
//    }
//}

