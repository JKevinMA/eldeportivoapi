using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class Transportista
    {
        public string IdTransportista{ get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Licencia { get; set; }
        public string Placa { get; set; }
        public string Vehiculo { get; set; }
        public string Modelo { get; set; }
        public int IdDistrito { get; set; }
        public string Distrito { get; set; }
    }
}
