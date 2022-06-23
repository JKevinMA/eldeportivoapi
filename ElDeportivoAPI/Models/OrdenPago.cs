using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class OrdenPago
    {
        public string IdOrdenPago { get; set; }
        public string FechaGenerado { get; set; }
        public double ImporteTotal { get; set; }
    }
}
