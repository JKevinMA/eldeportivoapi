using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IProveedorRepository
    {
        public Result<List<Proveedor>> buscarProveedores(string campo, string valor);
        public Result<List<Proveedor>> obtenerProveedoresCotizacion(string idSolicitudCotizacion);
    }
}
