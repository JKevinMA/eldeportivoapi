using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface IMaterialRepository
    {
        public Result<List<Material>> obtenerMaterialDeficit(int idCategoria);
    }
}
