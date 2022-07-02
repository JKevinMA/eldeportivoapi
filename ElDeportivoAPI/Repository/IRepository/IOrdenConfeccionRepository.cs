using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IOrdenConfeccionRepository
    {
        public Result<OrdenConfeccion> obtenerNuevoNroOrdenConfeccion(string prefijo);
        public Result<int> registrarOrdenConfeccion(OrdenConfeccion orden);
    }
}
