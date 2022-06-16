using ElDeportivoAPI.Models;
using ElDeportivoAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository
{
    public class TransportistaRepository: ITransportistaRepository
    {
        private Connection _bd;
        public Result<List<Transportista>> obtenerTransportistas(int idDistrito)
        {
            _bd = new Connection();
            List<Transportista> lista = new List<Transportista>();
            Result<List<Transportista>> r = new Result<List<Transportista>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerTransportistas, con);
                    sqlCommand.Parameters.Add("@idDistrito", SqlDbType.Int).Value = idDistrito;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Transportista
                            {
                                IdTransportista = reader["IdTransportista"].ToString(),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Licencia = reader["Licencia"].ToString(),
                                Placa = reader["Placa"].ToString(),
                                Vehiculo = reader["Vehiculo"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Distrito = reader["Distrito"].ToString(),
                                IdDistrito = int.Parse(reader["IdDistrito"].ToString())
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
