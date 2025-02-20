using API_VariasBDs.DTOs;
using API_VariasBDs.Models;
using API_VariasBDs.Repositorios;
using API_VariasBDs.Servicos.Abstracoes;

namespace API_VariasBDs.Servicos
{
    public class UserServico : IUserServico
    {
        public List<UserDTO> Users(string nomeBD)
        {
            var sql = "SELECT Id as IdUser, Nome, [Data], Token FROM BD1.dbo.[User];";
            var resposta = DapperRepositorio.Query<UserDTO>(nomeBD, sql);
            return resposta;
        }
    }
}
