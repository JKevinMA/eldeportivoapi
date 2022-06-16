using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IGuiaRemisionRepository
    {
        public Result<GuiaRemision> obtenerNuevoNroGuiaRemision(string prefijo);
        public Result<int> registrarGuiaRemision(GuiaRemision guia);
    }
}
