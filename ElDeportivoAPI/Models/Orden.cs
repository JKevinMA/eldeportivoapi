using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class Orden
    {
        public string IdOrden { get; set; }
        public string Concepto { get; set; }
        public int IdSolicitante { get; set; }
        public DateTime FechaGenerada { get; set; }
        public string Estado { get; set; }
        public int IdArea { get; set; }

        public int NroItems { get; set; }
        public List<OrdenDetalle> Detalles { get; set; }
    }

    public class OrdenDetalle 
    {
        public string IdOrden { get; set; }
        public string CodigoMaterial { get; set; }
        public double CantidadRequerida { get; set; }
        public string Material { get; set; }
        public string Presentacion { get; set; }
        public double CantidadSalida { get; set; }
    }
}
