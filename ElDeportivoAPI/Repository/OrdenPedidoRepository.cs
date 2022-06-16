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
    public class OrdenPedidoRepository: IOrdenPedidoRepository
    {
        private Connection _bd;
        public Result<List<OrdenPedido>> obtenerOrdenesPedido(string fecha,string estado)
        {
            _bd = new Connection();
            List<OrdenPedido> lista = new List<OrdenPedido>();
            Result<List<OrdenPedido>> r = new Result<List<OrdenPedido>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerOrdenesPedidos, con);
                    sqlCommand.Parameters.Add("@FECHA", SqlDbType.NVarChar).Value = fecha;
                    sqlCommand.Parameters.Add("@ESTADO", SqlDbType.NVarChar).Value = estado;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrdenPedido
                            {
                                IdOrdenPedido = reader["IdOrdenPedido"].ToString(),
                                IdCotizacion = reader["IdCotizacion"].ToString(),
                                FechaRecepcion = DateTime.Parse(reader["FechaRecepcion"].ToString()),
                                FechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString()),
                                Estado = reader["Estado"].ToString(),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Distrito = reader["Distrito"].ToString()
                            });
                        }
                    }

                    foreach (var item in lista)
                    {
                        item.Detalles = obtenerCotizacionDetalles(item.IdCotizacion);
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
        public List<CotizacionDetalle> obtenerCotizacionDetalles(string idCotizacion)
        {
            _bd = new Connection();
            List<CotizacionDetalle> lista = new List<CotizacionDetalle>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerCotizacionDetalle, con);
                    sqlCommand.Parameters.Add("@idCotizacion", SqlDbType.NVarChar).Value = idCotizacion;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CotizacionDetalle
                            {
                                IdCotizacion = reader["IdCotizacion"].ToString(),
                                Prenda = reader["Prenda"].ToString(),
                                Cantidad = int.Parse(reader["Cantidad"].ToString()),
                                Talla = reader["Talla"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Save(this, ex.ToString()) ;
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
