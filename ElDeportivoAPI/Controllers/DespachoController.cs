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
    public class DespachoController : ControllerBase
    {
        private readonly IDespachoRepository despachoRepo;

        public DespachoController(IDespachoRepository despachoRepo)
        {
            this.despachoRepo = despachoRepo;
        }

        [HttpGet("guias-remision")]
        public IActionResult Get(string fecha, string estado)
        {
            try
            {
                var res = despachoRepo.obtenerGuiasRemision(fecha, estado);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("nro-despacho")]
        public IActionResult GetNroDespacho(string prefijo)
        {
            try
            {
                var res = despachoRepo.obtenerNuevoNroDespacho(prefijo);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostGuiaRemision(Despacho despacho)
        {
            try
            {
                var res = despachoRepo.registrarDespacho(despacho);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
