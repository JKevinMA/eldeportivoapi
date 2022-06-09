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
    public class SolicitudCotizacionController : ControllerBase
    {
        private readonly IOrdenRepository ordenRepo;
        private readonly IProveedorRepository proveedorRepo;
        private readonly ISolicitudCotizacionRepository solicitudCotizacionRepo;

        public SolicitudCotizacionController(IOrdenRepository ordenRepo,IProveedorRepository proveedorRepo,ISolicitudCotizacionRepository solicitudCotizacionRepo)
        {
            this.ordenRepo = ordenRepo;
            this.proveedorRepo = proveedorRepo;
            this.solicitudCotizacionRepo = solicitudCotizacionRepo;
        }

        [HttpGet("ordenes")]
        public IActionResult GetOrdenesReposicion(string concepto,string estado)
        {
            try
            {
                var res = ordenRepo.obtenerOrdenes(concepto,estado);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("detalles-orden")]
        public IActionResult GetDetallesOrden(string idOrden)
        {
            try
            {
                var res = ordenRepo.obtenerDetallesOrden(idOrden);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("proveedores")]
        public IActionResult GetProveedores(string campo, string valor)
        {
            try
            {
                var res = proveedorRepo.buscarProveedores(campo, valor);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("orden-estado")]
        public IActionResult PutOrdenEstado(string estado, string idOrden)
        {
            try
            {
                var res = ordenRepo.actualizarEstadoOrden(estado, idOrden);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] SolicitudCotizacion solicitudCotizacion)
        {
            try
            {
                var res = solicitudCotizacionRepo.registrarSolicitudCotizacion(solicitudCotizacion);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("nro-solicitud-cotizacion")]
        public IActionResult Get(string prefijo)
        {
            try
            {
                var res = solicitudCotizacionRepo.obtenerNuevoNroSolicitudCotizacion(prefijo);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
