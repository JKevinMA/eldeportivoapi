using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class OrdenConfeccion
    {
        public string IdOrdenConfeccion { get; set; }
        public DateTime FechaGenerado { get; set; }
        public string TipoProduccion { get; set; }
        public string Estado { get; set; }
        public int IdTrabajador { get; set; }
        public string IdOrdenPedido { get; set; }
        public string IdFichaES { get; set; }
        public List<OrdenConfeccionDetalle> Detalles { get; set; }
    }
    public class OrdenConfeccionDetalle
    {
        public string IdOrdenConfeccion { get; set; }
        public string IdCotizacion { get; set; }
        public int IdPrenda { get; set; }
        public string CodigoMaterial{ get; set; }
        public int IdTalla { get; set; }
        public int IdDetalleDiseno { get; set; }
        public int CantidadFabricar { get; set; }
    }
}
