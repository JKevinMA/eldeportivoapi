using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class Cotizacion
    {
        public string IdCotizacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string NroDocumento { get; set; }
        public List<CotizacionDetalle> Detalles { get; set; }
    }
    public class CotizacionDetalle
    {
        public string IdCotizacion { get; set; }
        public int IdPrenda { get; set; }
        public string Prenda { get; set; }
        public string CodigoMaterial { get; set; }
        public string Material { get; set; }
        public int IdTalla { get; set; }
        public string Talla { get; set; }
        public int IdDetalleDiseno { get; set; }
        public string DetalleDiseno { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioFinal { get; set; }
    }
}
