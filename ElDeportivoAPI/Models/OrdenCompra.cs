using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class OrdenCompra
    {
        public string IdOrdenCompra { get; set; }
        public string IdSolicitudCotizacion { get; set; }
        public string Ruc { get; set; }
        public double CostoEnvio { get; set; }
        public double Subtotal { get; set; }
        public double Impuesto { get; set; }
        public DateTime FechaGenerada { get; set; }
        public int IdTrabajador { get; set; }
        public string RutaProforma { get; set; }
        public string Estado { get; set; }
        public List<OrdenCompraDetalle> Detalles { get; set; }
    }

    public class OrdenCompraDetalle
    {
        public string IdOrdenCompra { get; set; }
        public string CodigoMaterial { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
