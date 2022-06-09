using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface ISolicitudCotizacionRepository
    {
        public Result<SolicitudCotizacion> obtenerNuevoNroSolicitudCotizacion(string prefijo);
        public Result<int> registrarSolicitudCotizacion(SolicitudCotizacion sc);
        public Result<SolicitudCotizacion> obtenerSolicitudCotizacion(string idSolicitudCotizacion);
    }
}
