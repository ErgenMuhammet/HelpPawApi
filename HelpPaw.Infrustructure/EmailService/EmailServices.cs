

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
            var emailMessage = new MimeMessage(); //Mail içeriği, HTML gövdesi oluşturma yardımcısı

            emailMessage.From.Add(new MailboxAddress("HelpPaw Destek", _configuration["MailSettings:Email"])); // json dosyasından gönderen bilgisini alacağız
            emailMessage.To.Add(new MailboxAddress("", toEmail));

            emailMessage.Subject=subject;
            
            var bodyBuilder = new BodyBuilder // e posta gövdesi
            {
                HtmlBody = message
            }; // gövdede yukarıdan gelen bilgi bulunacak
            emailMessage.Body = bodyBuilder.ToMessageBody(); // yukarıdaki bilgiyi gerçek bir mesaja dönüştürüyor

            using (var client = new SmtpClient())
            {
                // SSL sertifika hatalarını yoksay (Geliştirme ortamı için)
                client.CheckCertificateRevocation = false;

                // SMTP Sunucusuna Bağlan (Gmail: smtp.gmail.com, Port: 587)
                await client.ConnectAsync(
                    _configuration["MailSettings:Host"],
                    int.Parse(_configuration["MailSettings:Port"]),
                    SecureSocketOptions.StartTls
                );

                // Oturum Aç (Email ve Uygulama Şifresi)
                await client.AuthenticateAsync(
                    _configuration["MailSettings:Email"],
                    _configuration["MailSettings:Password"]
                );

                // Gönder
                await client.SendAsync(emailMessage);

                // Bağlantıyı Kes
                await client.DisconnectAsync(true);
            }
        }
        
    }
}
