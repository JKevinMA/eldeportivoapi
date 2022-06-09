using ElDeportivoAPI.Models;
using ElDeportivoAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElDeportivoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenReposicionController : ControllerBase
    {
        private readonly ICategoriaRepository repoCategoria;
        private readonly IMaterialRepository repoMaterial;
        private readonly IOrdenRepository repoOrden;

        public OrdenReposicionController(ICategoriaRepository repoCategoria,IMaterialRepository repoMaterial,IOrdenRepository repoOrden)
        {
            this.repoCategoria = repoCategoria;
            this.repoMaterial = repoMaterial;
            this.repoOrden = repoOrden;
        }

        [HttpGet("categorias")]
        public IActionResult Get()
        {
            try
            {
                var res = repoCategoria.obtenerCategorias();
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("materiales-deficit")]
        public IActionResult Get(int idCategoria)
        {
            try
            {
                var res = repoMaterial.obtenerMaterialDeficit(idCategoria);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("nro-orden")]
        public IActionResult Get(string prefijo,string concepto)
        {
            try
            {
                var res = repoOrden.obtenerNuevoNroOrden(prefijo,concepto);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<OrdenReposicionController>
        [HttpPost]
        public IActionResult Post([FromBody] Orden orden)
        {
            try
            {
                var res = repoOrden.registrarOrden(orden);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
