using ElDeportivoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository.IRepository
{
    public interface ITransportistaRepository
    {
        public Result<List<Transportista>> obtenerTransportistas(int idDistrito);
    }
}
