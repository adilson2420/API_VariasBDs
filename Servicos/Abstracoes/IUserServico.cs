using API_VariasBDs.DTOs;

namespace API_VariasBDs.Servicos.Abstracoes
{
    public interface IUserServico

    {
        List<UserDTO> GetUsersDapper(HttpRequest request);
        List<UserDTO> UsersDapper(string nomeBD);
        Task<List<UserDTO>> UsersEntity(string nomeBD);
    }
}
