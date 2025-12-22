using HelpPawApi.Application.Interfaces;
using HelpPawApi.Domain.Entities.AppUser;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HelpPaw.Persistence.TokenService
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(AppUsers user)
        {
            var claims = new List<Claim>
            {
                // 1. ID (Sub): Kullanıcının benzersiz ID'si
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                
                // 2. Name: Kullanıcı Adı veya Email (User.Identity.Name burayı okur!)
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email), 
                
                // 3. Email: Açıkça email adresi
                new Claim(ClaimTypes.Email, user.Email),
                
                // 4. JTI: Token'ın benzersiz ID'si
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // 5. Ekstra Bilgiler (Ad, Soyad vb. - Frontend için gerekirse)
                new Claim("FullName", user.FullName ?? ""),
                
            };

            // Not: Rolleri burada eklemiyoruz, çünkü bu metot sadece User alıyor.
            // Rolleri eklemek istersen parametre olarak List<string> roles da alman gerekir.

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       
        
    }
}

