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
    public class OrdenCompraController : ControllerBase
    {
        private readonly IOrdenCompraRepository ordenCompraRepo;
        private readonly ISolicitudCotizacionRepository solicitudCotizacionRepo;
        private readonly IOrdenRepository ordenRepo;
        private readonly IProveedorRepository proveedorRepo;

        public OrdenCompraController(IOrdenCompraRepository ordenCompraRepo,ISolicitudCotizacionRepository solicitudCotizacionRepo,IOrdenRepository ordenRepo,IProveedorRepository proveedorRepo)
        {
            this.ordenCompraRepo = ordenCompraRepo;
            this.solicitudCotizacionRepo = solicitudCotizacionRepo;
            this.ordenRepo = ordenRepo;
            this.proveedorRepo = proveedorRepo;
        }

        [HttpGet("nro-orden-compra")]
        public IActionResult Get(string prefijo)
        {
            try
            {
                var res = ordenCompraRepo.obtenerNuevoNroOrdenCompra(prefijo);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("solicitud-cotizacion")]
        public IActionResult GetSolicitudCotizacion(string idSolicitudCotizacion)
        {
            try
            {
                var res = solicitudCotizacionRepo.obtenerSolicitudCotizacion(idSolicitudCotizacion);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("proveedores-cotizacion")]
        public IActionResult GetProveedoresCotizacion(string idSolicitudCotizacion)
        {
            try
            {
                var res = proveedorRepo.obtenerProveedoresCotizacion(idSolicitudCotizacion);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostOrdenCompra(OrdenCompra ordenCompra)
        {
            try
            {
                var res = ordenCompraRepo.registrarOrdenCompra(ordenCompra);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
