using ElDeportivoAPI.Helpers;
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
    public class FichaESRepository: IFichaESRepository
    {
        private Connection _bd;
        public Result<List<FichaES>> obtenerFichasES(string fecha, string estado)
        {
            _bd = new Connection();
            List<FichaES> lista = new List<FichaES>();
            Result<List<FichaES>> r = new Result<List<FichaES>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    var consulta = Queries.ObtenerFichasES;
                    if (fecha != "-")
                    {
                        consulta = Queries.ObtenerFichasESFecha;
                    }
                    SqlCommand sqlCommand = new SqlCommand(consulta, con);
                    sqlCommand.Parameters.Add("@FECHA", SqlDbType.NVarChar).Value = fecha;
                    sqlCommand.Parameters.Add("@ESTADO", SqlDbType.NVarChar).Value = estado;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new FichaES
                            {
                                IdFichaES = reader["IdFichaES"].ToString(),
                                IdOrdenReposicion = reader["IdOrdenReposicion"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                Concepto = reader["Concepto"].ToString(),
                                FechaGenerada = DateTime.Parse(reader["FechaGenerada"].ToString()),
                            });
                        }
                    }

                    foreach (var item in lista)
                    {
                        item.Detalles = obtenerOrdenReposicionDetalles(item.IdOrdenReposicion);
                    }
                    r.Success = true;
                    r.Response = lista;

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

        public List<OrdenDetalle> obtenerOrdenReposicionDetalles(string idOrdenReposicion)
        {
            _bd = new Connection();
            List<OrdenDetalle> lista = new List<OrdenDetalle>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerDetalleOrden, con);
                    sqlCommand.Parameters.Add("@idOrden", SqlDbType.NVarChar).Value = idOrdenReposicion;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrdenDetalle
                            {
                                IdOrden = reader["IdOrden"].ToString(),
                                CodigoMaterial = reader["CodigoMaterial"].ToString(),
                                CantidadRequerida = double.Parse(reader["CantidadRequerida"].ToString()),
                                Material = reader["Material"].ToString(),
                                Presentacion = reader["Presentacion"].ToString(),
                                CantidadSalida = double.Parse(reader["CantidadSalida"].ToString()),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Save(this, ex.ToString());
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }

            return lista;
        }
    }
}
