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
    public class OrdenCompraRepository: IOrdenCompraRepository
    {
        private Connection _bd;
        public Result<OrdenCompra> obtenerNuevoNroOrdenCompra(string prefijo)
        {
            _bd = new Connection();
            Result<OrdenCompra> r = new Result<OrdenCompra>();
            OrdenCompra o = new OrdenCompra();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerNuevoNroOrdenCompra, con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@prefijo", SqlDbType.NVarChar).Value = prefijo;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            o.IdOrdenCompra = reader["IdOrdenCompra"].ToString();
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
        public Result<int> registrarOrdenCompra(OrdenCompra orden)
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
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarNuevaOrdenCompra, con);
                    sqlCommand.Parameters.Add("@IdOrdenCompra", SqlDbType.NVarChar).Value = orden.IdOrdenCompra;
                    sqlCommand.Parameters.Add("@IdSolicitudCotizacion", SqlDbType.NVarChar).Value = orden.IdSolicitudCotizacion;
                    sqlCommand.Parameters.Add("@Ruc", SqlDbType.NVarChar).Value = orden.Ruc;
                    sqlCommand.Parameters.Add("@CostoEnvio", SqlDbType.Decimal).Value = orden.CostoEnvio;
                    sqlCommand.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = orden.Subtotal;
                    sqlCommand.Parameters.Add("@Impuesto", SqlDbType.Decimal).Value = orden.Impuesto;
                    sqlCommand.Parameters.Add("@RutaProforma", SqlDbType.NVarChar).Value = orden.RutaProforma;
                    sqlCommand.Parameters.Add("@IdTrabajador", SqlDbType.Int).Value = orden.IdTrabajador;
                    sqlCommand.Parameters.Add("@Estado", SqlDbType.NVarChar).Value = orden.Estado;
                    sqlCommand.Connection = con;
                    con.Open();

                    resOrden = sqlCommand.ExecuteNonQuery();

                    if (resOrden != 0)
                    {
                        resDetalle = 0;
                        orden.Detalles.ForEach(detalle => {
                            resDetalle += registrarOrdenCompraDetalle(detalle);
                        });
                        if (resDetalle == orden.Detalles.Count)
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
        int registrarOrdenCompraDetalle(OrdenCompraDetalle detalle)
        {
            int res = 0;
            _bd = new Connection();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarDetalleOrdenCompra, con);
                    sqlCommand.Parameters.Add("@IdOrdenCompra", SqlDbType.NVarChar).Value = detalle.IdOrdenCompra;
                    sqlCommand.Parameters.Add("@CodigoMaterial", SqlDbType.NVarChar).Value = detalle.CodigoMaterial;
                    sqlCommand.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = detalle.Cantidad;
                    sqlCommand.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;
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

        public Result<List<OrdenCompra>> obtenerOrdenesCompra()
        {
            _bd = new Connection();
            List<OrdenCompra> lista = new List<OrdenCompra>();
            Result<List<OrdenCompra>> r = new Result<List<OrdenCompra>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerOrdenesCompra, con);
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrdenCompra
                            {
                                IdOrdenCompra = reader["IdOrdenCompra"].ToString(),
                                FechaGenerada = DateTime.Parse(reader["FechaGenerada"].ToString()),
                                ImporteTotal = double.Parse(reader["ImporteTotal"].ToString()),
                                CostoEnvio = double.Parse(reader["CostoEnvio"].ToString()),
                                ModalidadPago = reader["ModalidadPago"].ToString(),
                                Ruc = reader["Ruc"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                RazonSocial = reader["RazonSocial"].ToString(),
                                Direccion = reader["Direccion"].ToString()
                            });
                        }
                    }

                    foreach (var item in lista)
                    {
                        item.Detalles = obtenerOrdenCompraDetalles(item.IdOrdenCompra);
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

        public List<OrdenCompraDetalle> obtenerOrdenCompraDetalles(string idOrdenCompra)
        {
            _bd = new Connection();
            List<OrdenCompraDetalle> lista = new List<OrdenCompraDetalle>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerOrdenesCompraDetalle, con);
                    sqlCommand.Parameters.Add("@idOrdenCompra", SqlDbType.NVarChar).Value = idOrdenCompra;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new OrdenCompraDetalle
                            {
                                IdOrdenCompra = reader["IdOrdenCompra"].ToString(),
                                CodigoMaterial = reader["CodigoMaterial"].ToString(),
                                Cantidad = int.Parse(reader["Cantidad"].ToString()),
                                PrecioUnitario = double.Parse(reader["PrecioUnitario"].ToString()),
                                Material = reader["Material"].ToString()
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
