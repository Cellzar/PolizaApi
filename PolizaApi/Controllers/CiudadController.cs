using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PolizaDB.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PolizaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CiudadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AutosContext _context;

        public CiudadController(IConfiguration configuration, AutosContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> getCiudad()
        {
            try
            {
                var ciudades = _context.Ciudads.Where(c => c.Estado == "ACT").ToList();

                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
