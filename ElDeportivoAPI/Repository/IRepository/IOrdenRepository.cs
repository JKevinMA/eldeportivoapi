using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IOrdenRepository
    {
        public Result<Orden> obtenerNuevoNroOrden(string prefijo, string concepto);
        public Result<int> registrarOrden(Orden orden);

        public Result<List<Orden>> obtenerOrdenes(string concepto, string estado);
        public Result<int> actualizarEstadoOrden(string estado, string idOrden);
        public Result<List<OrdenDetalle>> obtenerDetallesOrden(string idOrden);

    }
}
