using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class Despacho
    {
        public string IdDespacho { get; set; }
        public string FechaSalida { get; set; }
        public string OrigenDespacho { get; set; }
        public List<DespachoDetalle> Detalles { get; set; }
    }
    public class DespachoDetalle
    {
        public string IdDespacho { get; set; }
        public string IdGuiaRemision { get; set; }
    }
}
