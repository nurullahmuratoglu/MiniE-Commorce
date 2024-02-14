using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniE_Commerce.Domain.Entities.Identity;
using MiniE_Commorce.Application.Dtos.Token;
using MiniE_Commorce.Application.Interfaces.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using MiniE_Commerce.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace MiniE_Commerce.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private TokenSettings _tokenSettings;
        public TokenHandler(IConfiguration configuration,IOptions<TokenSettings> options)

        {
            _configuration = configuration;
            _tokenSettings = options.Value;
        }

        public TokenDto CreateAccessToken(AppUser appUser)
        {
            TokenDto token = new();

            //Security Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            token.AccessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenSettings.TokenExpirationMunitues);
            JwtSecurityToken securityToken = new(
                audience: _tokenSettings.Audience,
                issuer: _tokenSettings.Issuer,
                expires: token.AccessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, appUser.UserName), new(ClaimTypes.NameIdentifier, appUser.Id.ToString()) }
                
                );

            //Token oluşturucu sınıfından bir örnek alalım.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            //string refreshToken = CreateRefreshToken();

            token.RefreshToken = CreateRefreshToken();
            token.RefreshTokenExpiratioMunitues = _tokenSettings.RefreshTokenExpiratioMunitues;
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
