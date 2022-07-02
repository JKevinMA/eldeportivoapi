using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class FichaES
    {
        public string IdFichaES { get; set; }
        public string IdOrdenReposicion { get; set; }
        public string Estado { get; set; }
        public string Concepto { get; set; }
        public DateTime FechaGenerada { get; set; }

        public List<OrdenDetalle>Detalles{ get; set; }

    }
}
