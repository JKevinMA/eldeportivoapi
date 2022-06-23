using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IOrdenCompraRepository
    {
        public Result<OrdenCompra> obtenerNuevoNroOrdenCompra(string prefijo);
        public Result<int> registrarOrdenCompra(OrdenCompra orden);
        public Result<List<OrdenCompra>> obtenerOrdenesCompra();
    }
}
