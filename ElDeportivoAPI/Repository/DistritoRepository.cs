using ElDeportivoAPI.Models;
using ElDeportivoAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository
{
    public class DistritoRepository: IDistritoRepository
    {
        private Connection _bd;
        public Result<List<Distrito>> obtenerDistritos()
        {
            _bd = new Connection();
            List<Distrito> lista = new List<Distrito>();
            Result<List<Distrito>> r = new Result<List<Distrito>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerDistritos, con);
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Distrito
                            {
                                IdDistrito = int.Parse(reader["IdDistrito"].ToString()),
                                Descripcion = reader["Descripcion"].ToString(),
                            });
                        }
                        r.Success = true;
                        r.Response = lista;
                    }
                }
                catch (Exception ex)
                {
                    r.Success = false;
                    r.Message = ex.Message;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return r;
        }
    }
}
