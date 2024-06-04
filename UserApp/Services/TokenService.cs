using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApp.DTO;
using UserApp.Model;
using UserApp.Security;

namespace UserApp.Services
{
    public class TokenService(JwtConfiguration jwt):ITokenService
    {
        public string GenerateToken(MailRoleDTO mailRoleDTO)
        {
            var securityKey = new RsaSecurityKey(RSATools.GetPrivateKey());
            var credentilas = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, mailRoleDTO.Email),
                new Claim(ClaimTypes.Role, mailRoleDTO.Role.ToString())
            };
            var token = new JwtSecurityToken
            (
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentilas
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
