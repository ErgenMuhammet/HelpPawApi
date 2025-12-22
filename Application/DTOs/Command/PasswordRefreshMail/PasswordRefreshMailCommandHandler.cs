using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppUser;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpPawApi.Application.DTOs.Command.PasswordRefreshMail
{
    public class PasswordRefreshMailCommandHandler : IRequestHandler<PasswordRefreshMailCommandRequest, PasswordRefreshMailCommandResponse>
    {
        private readonly IEmailServices _emailServices;

        public PasswordRefreshMailCommandHandler(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }
        public async Task<PasswordRefreshMailCommandResponse> Handle(PasswordRefreshMailCommandRequest request, CancellationToken cancellationToken)
        {            
            
                var rndm = new Random();

                int ConfirmCode = rndm.Next(100000, 999999);


            try
            {
                await _emailServices.SendEmailAsync(request.Email,
                                                    "Doğrulama Kodu",
                                                    $"{ConfirmCode.ToString()} kodu ile şifrenizi yenileyebilirsiniz." );
                }
                catch (Exception ex)
                {
                    return new PasswordRefreshMailCommandResponse
                    {
                        Errors = ex.Message,
                        IsSucces = false,
                        Message = "Doğrulama kodu gönderilemedi.",
                        ConfirmCode = string.Empty,

                    };
                }

                return new PasswordRefreshMailCommandResponse
                {
                    Errors = "",
                    IsSucces = true,
                    Message = "Email hesabınızdaki doğrulama kodunu giriniz.",
                    ConfirmCode = ConfirmCode.ToString(),

                };
            

        }
    }
}
