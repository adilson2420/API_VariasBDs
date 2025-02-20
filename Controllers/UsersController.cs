using API_VariasBDs.DTOs;
using API_VariasBDs.Models;
using API_VariasBDs.Servicos.Abstracoes;
using Microsoft.AspNetCore.Mvc;

namespace API_VariasBDs.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserServico _userServico;

        public UsersController(ILogger<UsersController> logger, IUserServico userServico)
        {
            _logger = logger;
            _userServico = userServico;
        }
        [HttpPost("Gerartoken")]
        public ActionResult<string> GetToken(string nomeBD)
        {
            string token = TokenServico.GenerateToken(nomeBD);
            return Ok(token);
        }

        [HttpGet(Name = "Listar utilizadores")]
        public IActionResult GetUsers()
        {
            try
            {
                var authorizationHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                    return StatusCode(401, "Token não encontrado ou inválido");

                string token = authorizationHeader.Substring("Bearer ".Length).Trim();

                // Extrai o nome da base de dados do token
                string dbName = TokenHelper.GetDatabaseFromToken(token);
                List<UserDTO> users = _userServico.Users(dbName);
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Falha ao listar users");
            }
        }
    }
}
