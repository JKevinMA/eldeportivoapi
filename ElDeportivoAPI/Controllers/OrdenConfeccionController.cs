using ElDeportivoAPI.Models;
using ElDeportivoAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenConfeccionController : ControllerBase
    {
        private readonly IOrdenPedidoRepository ordenPedidoRepo;
        private readonly IOrdenConfeccionRepository ordenConfeccionRepo;
        private readonly IFichaESRepository fichaESRepo;

        public OrdenConfeccionController(IOrdenPedidoRepository ordenPedidoRepo,IOrdenConfeccionRepository ordenConfeccionRepo,IFichaESRepository fichaESRepo)
        {
            this.ordenPedidoRepo = ordenPedidoRepo;
            this.ordenConfeccionRepo = ordenConfeccionRepo;
            this.fichaESRepo = fichaESRepo;
        }

        [HttpGet("nro-orden-confeccion")]
        public IActionResult GetNroGuiaRemision(string prefijo)
        {
            try
            {
                var res = ordenConfeccionRepo.obtenerNuevoNroOrdenConfeccion(prefijo);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("fichases")]
        public IActionResult Get(string fecha, string estado)
        {
            try
            {
                var res = fichaESRepo.obtenerFichasES(fecha, estado);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(OrdenConfeccion orden)
        {
            try
            {
                var res = ordenConfeccionRepo.registrarOrdenConfeccion(orden);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
