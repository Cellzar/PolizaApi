using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PolizaDB.Context;
using PolizaDB.DTOs;
using System;
using System.Threading.Tasks;

namespace PolizaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class PolizaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AutosContext _context;

        public PolizaController(IConfiguration configuration, AutosContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Response>> savePoliza(PolizaDto poliza)
        {
            Response respuesta = new Response();

            try
            {
                respuesta = _context.GuardarPoliza(poliza);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = ex.Message;
                return respuesta;
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Response>> getPolizas(int numeroPoliza, string placa)
        {
            Response respuesta = new Response();

            try
            {
                respuesta = _context.ObtenerPoliza(numeroPoliza, placa);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = ex.Message;
                return respuesta;
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Response>> getPolizasFechas(DateTime fechaInicial, DateTime fechaFinal)
        {
            Response respuesta = new Response();

            try
            {
                respuesta = _context.ObtenerTodasPolizasFecha(fechaInicial, fechaFinal);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = ex.Message;
                return respuesta;
            }
        }
    }
}
