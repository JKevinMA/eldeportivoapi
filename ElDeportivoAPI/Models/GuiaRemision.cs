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
    }
}
