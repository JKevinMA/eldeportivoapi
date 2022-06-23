using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IDespachoRepository
    {
        public Result<Despacho> obtenerNuevoNroDespacho(string prefijo);
        public Result<int> registrarDespacho(Despacho despacho);
        public Result<List<GuiaRemision>> obtenerGuiasRemision(string fecha, string estado);
    }
}
