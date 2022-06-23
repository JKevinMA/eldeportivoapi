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
    public class RecepcionCompraController : ControllerBase
    {
        private readonly IOrdenCompraRepository ordenCompraRepo;
        private readonly IOrdenPagoRepository ordenPagoRepo;

        public RecepcionCompraController(IOrdenCompraRepository ordenCompraRepo,IOrdenPagoRepository ordenPagoRepo)
        {
            this.ordenCompraRepo = ordenCompraRepo;
            this.ordenPagoRepo = ordenPagoRepo;
        }

        [HttpGet("ordenes-compra")]
        public IActionResult Get()
        {
            try
            {
                var res = ordenCompraRepo.obtenerOrdenesCompra();
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("nro-orden-pago")]
        public IActionResult Get(string prefijo)
        {
            try
            {
                var res = ordenPagoRepo.obtenerNuevoNroOrdenPago(prefijo);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
