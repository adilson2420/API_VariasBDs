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

        [HttpGet(Name = "Listar utilizadores")]
        public IActionResult GetUsers()
        {
            try
            {
                List<UserDTO> users = _userServico.Users();
                return StatusCode(200, users);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Falha ao listar users");
            }
        }
    }
}
