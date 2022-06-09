using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Models
{
    public class Material
    {
        public string CodigoMaterial { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Marca { get; set; }
        public string Presentacion { get; set; }
        public double Stock { get; set; }
        public double Limite { get; set; }
        public double CantidadMinima { get; set; }
        public double Precio { get; set; }
        public int IdCategoria { get; set; }

        //AUX
        public string Categoria { get; set; }
    }
}
