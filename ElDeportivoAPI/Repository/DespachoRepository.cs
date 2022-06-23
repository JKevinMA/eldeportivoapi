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
    public class DespachoRepository: IDespachoRepository
    {
        private Connection _bd;
        public Result<Despacho> obtenerNuevoNroDespacho(string prefijo)
        {
            _bd = new Connection();
            Result<Despacho> r = new Result<Despacho>();
            Despacho o = new Despacho();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerNuevoNroDespacho, con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@prefijo", SqlDbType.NVarChar).Value = prefijo;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            o.IdDespacho = reader["IdDespacho"].ToString();
                        }
                        r.Success = true;
                        r.Response = o;
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
        public Result<int> registrarDespacho(Despacho despacho)
        {
            _bd = new Connection();
            Result<int> r = new Result<int>();
            int resOrden = 0;
            int resDetalle = 0;
            int res = 0;
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarNuevoDespacho, con);
                    sqlCommand.Parameters.Add("@IdDespacho", SqlDbType.NVarChar).Value = despacho.IdDespacho;
                    sqlCommand.Parameters.Add("@OrigenDespacho", SqlDbType.NVarChar).Value = despacho.OrigenDespacho;
                    sqlCommand.Connection = con;
                    con.Open();

                    resOrden = sqlCommand.ExecuteNonQuery();

                    if (resOrden != 0)
                    {
                        resDetalle = 0;
                        despacho.Detalles.ForEach(detalle => {
                            resDetalle += registrarDespachoDetalle(detalle);
                        });
                        if (resDetalle == despacho.Detalles.Count)
                        {
                            res = 1;
                        }
                    }

                    r.Success = true;
                    r.Response = res;
                }
                catch (Exception ex)
                {
                    r.Success = false;
                    r.Message = ex.Message;
                    r.Response = 0;
                    Log.Save(this, ex.ToString());
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return r;
        }

        int registrarDespachoDetalle(DespachoDetalle detalle)
        {
            int res = 0;
            _bd = new Connection();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarDetalleDespacho, con);
                    sqlCommand.Parameters.Add("@IdDespacho", SqlDbType.NVarChar).Value = detalle.IdDespacho;
                    sqlCommand.Parameters.Add("@IdGuiaRemision", SqlDbType.NVarChar).Value = detalle.IdGuiaRemision;
                    sqlCommand.Connection = con;
                    con.Open();

                    res = sqlCommand.ExecuteNonQuery();

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
                return res;
            }
        }

        public Result<List<GuiaRemision>> obtenerGuiasRemision(string fecha, string estado)
        {
            _bd = new Connection();
            List<GuiaRemision> lista = new List<GuiaRemision>();
            Result<List<GuiaRemision>> r = new Result<List<GuiaRemision>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerGuiasRemision, con);
                    sqlCommand.Parameters.Add("@FECHA", SqlDbType.NVarChar).Value = fecha;
                    sqlCommand.Parameters.Add("@ESTADO", SqlDbType.NVarChar).Value = estado;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new GuiaRemision
                            {
                                IdGuiaRemision = reader["IdGuiaRemision"].ToString(),
                                IdOrdenPedido = reader["IdOrdenPedido"].ToString(),
                                IdTransportista = reader["IdTransportista"].ToString(),
                                Vehiculo = reader["Vehiculo"].ToString(),
                                Modelo = reader["Modelo"].ToString(),
                                Placa = reader["Placa"].ToString(),
                                Estado = reader["Estado"].ToString(),
                                NombresCliente = reader["NombresCliente"].ToString(),
                                ApellidosCliente = reader["ApellidosCliente"].ToString(),
                                NombresTransportista = reader["NombresTransportista"].ToString(),
                                ApellidosTransportista = reader["ApellidosTransportista"].ToString(),
                                Direccion = reader["Direccion"].ToString(),
                                Distrito = reader["Distrito"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                IdCotizacion = reader["IdCotizacion"].ToString(),
                                FechaEntrega = DateTime.Parse(reader["FechaEntrega"].ToString()),
                                FechaCreacionGuia = DateTime.Parse(reader["FechaCreacionGuia"].ToString()),
                            });
                        }
                    }

                    OrdenPedidoRepository repo = new OrdenPedidoRepository();
                    foreach (var item in lista)
                    {
                        item.Detalles = repo.obtenerCotizacionDetalles(item.IdCotizacion);
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
        
    }
}
