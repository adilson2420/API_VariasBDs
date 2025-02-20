using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_VariasBDs
{
    public class TokenServico
    {
        public static string GenerateToken(string dbName)
        {
            var secretKey = "super secret key super secret key super secret key super secret key super secret key super secret key super secret key";

            // Tempo atual em segundos (Unix Timestamp)
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var expires = now + 3600;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Criando as informações do payload (dados do usuário)
            var claims = new[]
            {
            new Claim("dbName", dbName),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64), // Tempo de criação
            new Claim(JwtRegisteredClaimNames.Exp, expires.ToString(), ClaimValueTypes.Integer64) // Expiração
        };

            // Criando o token
            var token = new JwtSecurityToken(
                issuer: "MinhaAPI",
                audience: "MeusClientes",
                claims: claims,
                expires: DateTimeOffset.FromUnixTimeSeconds(expires).UtcDateTime,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
