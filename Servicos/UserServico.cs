using API_VariasBDs.DTOs;
using API_VariasBDs.Models;
using API_VariasBDs.Repositorios;
using API_VariasBDs.Servicos.Abstracoes;

namespace API_VariasBDs.Servicos
{
    public class UserServico : IUserServico
    {
        private string ExtrairDBDoRequest(HttpRequest request)
        {
            var authorizationHeader = request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                throw new Exception("Token não encontrado ou inválido!");

            string token = authorizationHeader.Substring("Bearer ".Length).Trim();
            return TokenHelper.GetDatabaseFromToken(token);
        }
        public List<UserDTO> GetUsersDapper(HttpRequest request)
        {
            string dbName = ExtrairDBDoRequest(request);
            if (dbName == null) throw new Exception("Nome da BD em falta no token");

            return UsersDapper(dbName);
        }

        public List<UserDTO> UsersDapper(string nomeBD)
        {
            var sql = "SELECT Id as IdUser, Nome, [Data], Token FROM dbo.[User];";
            var resposta = DapperRepositorio.Query<UserDTO>(nomeBD, sql);
            return resposta;
        }

        public async Task<List<UserDTO>> UsersEntity(string nomeBD)
        {
            throw new NotImplementedException();
        }
    }
}
