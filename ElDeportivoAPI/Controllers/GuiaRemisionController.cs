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
    public class GuiaRemisionController : ControllerBase
    {
        private readonly IOrdenPedidoRepository ordenPedidoRepo;
        private readonly ITransportistaRepository transportistaRepo;
        private readonly IDistritoRepository distritoRepo;
        private readonly IGuiaRemisionRepository remisionRepo;

        public GuiaRemisionController(IOrdenPedidoRepository ordenPedidoRepo,ITransportistaRepository transportistaRepo,IDistritoRepository distritoRepo,IGuiaRemisionRepository remisionRepo)
        {
            this.ordenPedidoRepo = ordenPedidoRepo;
            this.transportistaRepo = transportistaRepo;
            this.distritoRepo = distritoRepo;
            this.remisionRepo = remisionRepo;
        }

        [HttpGet("pedidos")]
        public IActionResult Get(string fecha,string estado)
        {
            try
            {
                var res = ordenPedidoRepo.obtenerOrdenesPedido(fecha,estado);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("transportistas")]
        public IActionResult GetTransportistas(int idDistrito)
        {
            try
            {
                var res = transportistaRepo.obtenerTransportistas(idDistrito);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("distritos")]
        public IActionResult GetDistritos()
        {
            try
            {
                var res = distritoRepo.obtenerDistritos();
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("nro-guia")]
        public IActionResult GetNroGuiaRemision(string prefijo)
        {
            try
            {
                var res = remisionRepo.obtenerNuevoNroGuiaRemision(prefijo);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostGuiaRemision(GuiaRemision guiaRemision)
        {
            try
            {
                var res = remisionRepo.registrarGuiaRemision(guiaRemision);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
