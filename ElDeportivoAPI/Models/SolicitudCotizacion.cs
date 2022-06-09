using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class SolicitudCotizacion
    {
        public string IdSolicitudCotizacion { get; set; }
        public int IdTrabajador { get; set; }
        public string ModalidadPago { get; set; }
        public DateTime FechaGenerada { get; set; }
        public DateTime FechaLimite { get; set; }
        public string IdOrden { get; set; }
        public List<ProveedorCotizacion> Proveedores { get; set; }
    }
    public class ProveedorCotizacion
    {
        public string IdSolicitudCotizacion { get; set; }
        public string Ruc { get; set; }
    }
}
