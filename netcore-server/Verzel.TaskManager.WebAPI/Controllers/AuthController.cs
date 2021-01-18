using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Verzel.TaskManager.WebAPI.Authentication;
using Verzel.TaskManager.WebAPI.Database;
using Verzel.TaskManager.WebAPI.DTO.Login;

namespace Verzel.TaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(ApiContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // POST api/<TarefaController>
        [HttpPost]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Senha != loginDTO.Senha)
            {
                return Unauthorized();
            }

            return Ok(new LoginResponseDTO() { Jwt = _tokenService.GenerateToken(user) });
        }
    }
}
