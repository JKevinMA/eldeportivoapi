using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IOrdenPedidoRepository
    {
        public Result<List<OrdenPedido>> obtenerOrdenesPedido(string fecha, string estado);
    }
}
