using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PolizaDB.Context;
using PolizaDB.DTOs;
using PolizaLogic.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AutosContext _context;
        TokenJWT token = new TokenJWT();

        public UserController(IConfiguration configuration, AutosContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> login(UserDto login)
        {
            Response respuesta = new Response();
            try
            {
                respuesta = _context.Login(login);

                if(respuesta.Mensaje == "Bienvenido al sistema")
                {
                    respuesta.Data = new { token = token.GenerateToken(login, _configuration) };
                }

                respuesta.EsError = false;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = ex.Message;
                return BadRequest(respuesta);
            }
        }
    }
}
