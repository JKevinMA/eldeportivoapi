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
    public class TrabajadorController : ControllerBase
    {
        private readonly ITrabajadorRepository trabajadorRepo;

        public TrabajadorController(ITrabajadorRepository trabajadorRepo)
        {
            this.trabajadorRepo = trabajadorRepo;
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] Trabajador user)
        {
            try
            {
                var res = trabajadorRepo.Login(user);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
