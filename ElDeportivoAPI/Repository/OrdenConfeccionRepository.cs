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
    public class OrdenConfeccionRepository: IOrdenConfeccionRepository
    {
        private Connection _bd;
        public Result<OrdenConfeccion> obtenerNuevoNroOrdenConfeccion(string prefijo)
        {
            _bd = new Connection();
            Result<OrdenConfeccion> r = new Result<OrdenConfeccion>();
            OrdenConfeccion o = new OrdenConfeccion();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerNuevoNroOrdenConfeccion, con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@prefijo", SqlDbType.NVarChar).Value = prefijo;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            o.IdOrdenConfeccion = reader["IdOrdenConfeccion"].ToString();
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

        public Result<int> registrarOrdenConfeccion(OrdenConfeccion orden)
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
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarNuevaOrdenConfeccion, con);
                    sqlCommand.Parameters.Add("@IdOrdenConfeccion", SqlDbType.NVarChar).Value = orden.IdOrdenConfeccion;
                    sqlCommand.Parameters.Add("@TipoProduccion", SqlDbType.NVarChar).Value = orden.TipoProduccion;
                    sqlCommand.Parameters.Add("@Estado", SqlDbType.NVarChar).Value = orden.Estado;
                    sqlCommand.Parameters.Add("@IdTrabajador", SqlDbType.Int).Value = orden.IdTrabajador;
                    sqlCommand.Parameters.Add("@IdOrdenPedido", SqlDbType.NVarChar).Value = orden.IdOrdenPedido;
                    sqlCommand.Parameters.Add("@IdFichaES", SqlDbType.NVarChar).Value = orden.IdFichaES;
                    sqlCommand.Connection = con;
                    con.Open();

                    resOrden = sqlCommand.ExecuteNonQuery();

                    if (resOrden != 0)
                    {
                        resDetalle = 0;
                        orden.Detalles.ForEach(detalle => {
                            resDetalle += registrarOrdenConfeccionDetalle(detalle);
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

        int registrarOrdenConfeccionDetalle(OrdenConfeccionDetalle detalle)
        {
            int res = 0;
            _bd = new Connection();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarDetalleOrdenConfeccion, con);
                    sqlCommand.Parameters.Add("@IdOrdenConfeccion", SqlDbType.NVarChar).Value = detalle.IdOrdenConfeccion;
                    sqlCommand.Parameters.Add("@IdCotizacion", SqlDbType.NVarChar).Value = detalle.IdCotizacion;
                    sqlCommand.Parameters.Add("@CodigoMaterial", SqlDbType.NVarChar).Value = detalle.CodigoMaterial;
                    sqlCommand.Parameters.Add("@IdPrenda", SqlDbType.Int).Value = detalle.IdPrenda;
                    sqlCommand.Parameters.Add("@IdTalla", SqlDbType.Int).Value = detalle.IdTalla;
                    sqlCommand.Parameters.Add("@IdDetalleDiseno", SqlDbType.Int).Value = detalle.IdDetalleDiseno;
                    sqlCommand.Parameters.Add("@CantidadFabricar", SqlDbType.Int).Value = detalle.CantidadFabricar;
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
    }
}
