using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class GuiaRemision
    {
        public string IdGuiaRemision { get; set; }
        public DateTime FechaEmision { get; set; }
        public string IdOrdenPedido { get; set; }
        public string IdTransportista { get; set; }
        public string Vehiculo { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Estado { get; set; }

        public string NombresCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string NombresTransportista { get; set; }
        public string ApellidosTransportista { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public string Telefono { get; set; }
        public string IdCotizacion { get; set; }
        public DateTime FechaCreacionGuia { get; set; }
        public DateTime FechaEntrega { get; set; }

        public List<CotizacionDetalle> Detalles { get; set; }

    }
}
