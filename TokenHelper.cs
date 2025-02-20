using System.IdentityModel.Tokens.Jwt;

namespace API_VariasBDs
{
    public static class TokenHelper
    {
        public static string GetDatabaseFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
                throw new UnauthorizedAccessException("Token inválido");

            var dbName = jsonToken.Claims.FirstOrDefault(c => c.Type == "dbName")?.Value;

            if (string.IsNullOrEmpty(dbName))
                throw new UnauthorizedAccessException("Banco de dados não especificado no token");

            return dbName;
        }
    }
}
