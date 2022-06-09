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
    public class OrdenRepository : IOrdenRepository
    {
        private Connection _bd;
        public Result<Orden> obtenerNuevoNroOrden(string prefijo,string concepto)
        {
            _bd = new Connection();
            Result<Orden> r = new Result<Orden>();
            Orden o = new Orden();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerNuevoNroOrden, con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@prefijo", SqlDbType.NVarChar).Value = prefijo;
                    sqlCommand.Parameters.Add("@concepto", SqlDbType.NVarChar).Value = concepto;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            o.IdOrden = reader["IDORDEN"].ToString();   
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
        public Result<int> registrarOrden(Orden orden)
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
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarNuevaOrden, con);
                    sqlCommand.Parameters.Add("@IdOrden", SqlDbType.NVarChar).Value = orden.IdOrden;
                    sqlCommand.Parameters.Add("@Concepto", SqlDbType.NVarChar).Value = orden.Concepto;
                    sqlCommand.Parameters.Add("@IdSolicitante", SqlDbType.Int).Value = orden.IdSolicitante;
                    sqlCommand.Parameters.Add("@NroItems", SqlDbType.Int).Value = orden.NroItems;
                    sqlCommand.Parameters.Add("@Estado", SqlDbType.NVarChar).Value = orden.Estado;
                    sqlCommand.Parameters.Add("@IdArea", SqlDbType.Int).Value = orden.IdArea;
                    sqlCommand.Connection = con;
                    con.Open();

                    resOrden = sqlCommand.ExecuteNonQuery();

                    if(resOrden != 0)
                    {
                        resDetalle = 0;
                        orden.Detalles.ForEach(detalle => {
                            resDetalle += registrarOrdenDetalle(detalle);
                        });
                        if(resDetalle == orden.Detalles.Count)
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
        int registrarOrdenDetalle(OrdenDetalle detalle)
        {
            int res = 0;
            _bd = new Connection();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarDetalleOrden, con);
                    sqlCommand.Parameters.Add("@IdOrden", SqlDbType.NVarChar).Value = detalle.IdOrden;
                    sqlCommand.Parameters.Add("@CodigoMaterial", SqlDbType.NVarChar).Value = detalle.CodigoMaterial;
                    sqlCommand.Parameters.Add("@CantidadRequerida", SqlDbType.Decimal).Value = detalle.CantidadRequerida;
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

        public Result<List<Orden>> obtenerOrdenes(string concepto,string estado)
        {
            _bd = new Connection();
            List<Orden> lista = new List<Orden>();
            Result<List<Orden>> r = new Result<List<Orden>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerOrdenes, con);
                    sqlCommand.Parameters.Add("@concepto", SqlDbType.NVarChar).Value = concepto;
                    sqlCommand.Parameters.Add("@estado", SqlDbType.NVarChar).Value = estado;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Orden
                            {
                                IdOrden = reader["IdOrden"].ToString(),
                                Concepto = reader["Concepto"].ToString(),
                                IdSolicitante = int.Parse(reader["IdSolicitante"].ToString()),
                                FechaGenerada = DateTime.Parse(reader["FechaGenerada"].ToString()),
                                Estado = reader["Estado"].ToString(),
                                IdArea = int.Parse(reader["IdArea"].ToString()),
                                NroItems = int.Parse(reader["NroItems"].ToString())
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

        public Result<List<OrdenDetalle>> obtenerDetallesOrden(string idOrden)
        {
            _bd = new Connection();
            List<OrdenDetalle> lista = new List<OrdenDetalle>();
            Result<List<OrdenDetalle>> r = new Result<List<OrdenDetalle>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerDetalleOrden, con);
                    sqlCommand.Parameters.Add("@idOrden", SqlDbType.NVarChar).Value = idOrden;
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
                                Presentacion = reader["Presentacion"].ToString()
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
        public Result<int> actualizarEstadoOrden(string estado,string idOrden)
        {
            _bd = new Connection();
            Result<int> r = new Result<int>();
            int res = 0;
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ActualizarEstadoOrden, con);
                    sqlCommand.Parameters.Add("@Estado", SqlDbType.NVarChar).Value = estado;
                    sqlCommand.Parameters.Add("@IdOrden", SqlDbType.NVarChar).Value = idOrden;
                    sqlCommand.Connection = con;
                    con.Open();

                    res = sqlCommand.ExecuteNonQuery();

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
    }
}
