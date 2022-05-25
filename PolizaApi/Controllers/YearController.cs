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
    public class YearController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AutosContext _context;

        public YearController(IConfiguration configuration, AutosContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> getYear()
        {
            try
            {
                var year = _context.Years.Where(c => c.Estado == "ACT").ToList();

                return Ok(year);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
