

using HelpPawApi.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NETCore.MailKit.Core;


namespace HelpPaw.Infrustructure.EmailService
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        public EmailServices(IConfiguration configuration)
        {
                _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("HelpPaw Destek", _configuration["MailSettings:Email"]));
            emailMessage.To.Add(new MailboxAddress("", toEmail));

            emailMessage.Subject=subject;
            
            var bodyBuilder = new BodyBuilder 
            {
                HtmlBody = message
            }; 
            emailMessage.Body = bodyBuilder.ToMessageBody(); 

            using (var client = new SmtpClient())
            {
                
                client.CheckCertificateRevocation = false;

                
                await client.ConnectAsync(
                    _configuration["MailSettings:Host"],
                    int.Parse(_configuration["MailSettings:Port"]),
                    SecureSocketOptions.StartTls
                );

                
                await client.AuthenticateAsync(
                    _configuration["MailSettings:Email"],
                    _configuration["MailSettings:Password"]
                );

              
                await client.SendAsync(emailMessage);

                
                await client.DisconnectAsync(true);
            }
        }
        
    }
}
