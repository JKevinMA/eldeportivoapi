using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class Trabajador
    {
        public int IdTrabajador { get; set; }
        public int Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public int IdRol { get; set; }
        public int IdArea { get; set; }

        //AUX
        public string Area { get; set; }
        public string Rol { get; set; }
    }
}
