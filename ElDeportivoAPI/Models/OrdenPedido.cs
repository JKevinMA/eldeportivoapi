using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class OrdenPedido
    {
        public string IdOrdenPedido { get; set; }
        public string IdCotizacion { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Distrito { get; set; }

        public List<CotizacionDetalle> Detalles { get; set; }
    }

}
