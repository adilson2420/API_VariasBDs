using API_VariasBDs.DTOs;
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

        [HttpGet("dapper", Name = "Listar utilizadores Dapper")]
        public IActionResult GetUsersDapper()
        {
            try
            {
                var users = _userServico.GetUsersDapper(Request);
                if (users == null) return StatusCode(404, "Não encontrado Utilizadores!");
                object obj = new
                {
                    StatusCode = 200,
                    Msg = "Lista de utilizadores com Dapper",
                    Resposta = users
                };
                return StatusCode(200, obj);
            }
            catch (Exception ex)
            {
                object obj = new
                {
                    StatusCode = 400,
                    erro = ex.Message,
                };
                return StatusCode(400, obj);
            }
        }

        [HttpGet("entity", Name = "Listar utilizadores Entity")]
        public async Task<IActionResult> GetUsersEntity()
        {
            try
            {
                List<UserDTO> users = await _userServico.UsersEntity("BD2");
                object obj = new
                {
                    StatusCode = 200,
                    Msg = "Lista de utilizadores com Entity Core",
                    Resposta = users
                };
                return StatusCode(200, obj);
            }
            catch (Exception ex)
            {
                object obj = new
                {
                    StatusCode = 400,
                    erro = ex.Message,
                };
                return StatusCode(400, obj);
            }
        }

    }
}
