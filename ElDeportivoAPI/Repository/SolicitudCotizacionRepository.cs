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
    public class SolicitudCotizacionRepository: ISolicitudCotizacionRepository
    {
        private Connection _bd;
        public Result<SolicitudCotizacion> obtenerNuevoNroSolicitudCotizacion(string prefijo)
        {
            _bd = new Connection();
            Result<SolicitudCotizacion> r = new Result<SolicitudCotizacion>();
            SolicitudCotizacion o = new SolicitudCotizacion();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerNuevoNroSolicitudCotizacion, con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@prefijo", SqlDbType.NVarChar).Value = prefijo;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            o.IdSolicitudCotizacion = reader["IdSolicitudCotizacion"].ToString();
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

        public Result<int> registrarSolicitudCotizacion(SolicitudCotizacion sc)
        {
            _bd = new Connection();
            Result<int> r = new Result<int>();
            int resSolicitudCotizacion = 0;
            int resProveedorCotizacion = 0;
            int res = 0;
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarSolicitudCotizacion, con);
                    sqlCommand.Parameters.Add("@Idsolicitudcotizacion", SqlDbType.NVarChar).Value = sc.IdSolicitudCotizacion;
                    sqlCommand.Parameters.Add("@IdTrabajador", SqlDbType.Int).Value = sc.IdTrabajador;
                    sqlCommand.Parameters.Add("@ModalidadPago", SqlDbType.NVarChar).Value = sc.ModalidadPago;
                    sqlCommand.Parameters.Add("@FechaLimite", SqlDbType.DateTime).Value = sc.FechaLimite;
                    sqlCommand.Parameters.Add("@IdOrden", SqlDbType.NVarChar).Value = sc.IdOrden;
                    sqlCommand.Connection = con;
                    con.Open();

                    resSolicitudCotizacion = sqlCommand.ExecuteNonQuery();

                    if (resSolicitudCotizacion != 0)
                    {
                        resProveedorCotizacion = 0;
                        sc.Proveedores.ForEach(proveedor => {
                            resProveedorCotizacion += registrarDetalleSolicitudCotizacion(proveedor);
                        });
                        if (resProveedorCotizacion == sc.Proveedores.Count)
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
        int registrarDetalleSolicitudCotizacion(ProveedorCotizacion proveedor)
        {
            int res = 0;
            _bd = new Connection();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarDetalleSolicitudCotizacion, con);
                    sqlCommand.Parameters.Add("@IdSolicitudCotizacion", SqlDbType.NVarChar).Value = proveedor.IdSolicitudCotizacion;
                    sqlCommand.Parameters.Add("@Ruc", SqlDbType.NVarChar).Value = proveedor.Ruc;
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

        public Result<SolicitudCotizacion> obtenerSolicitudCotizacion(string idSolicitudCotizacion)
        {
            _bd = new Connection();
            SolicitudCotizacion sc = new SolicitudCotizacion();
            Result<SolicitudCotizacion> r = new Result<SolicitudCotizacion>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerSolicitudCotizacion, con);
                    sqlCommand.Parameters.Add("@IdSolicitudCotizacion", SqlDbType.NVarChar).Value = idSolicitudCotizacion;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sc=new SolicitudCotizacion
                            {
                                IdSolicitudCotizacion = reader["IdSolicitudCotizacion"].ToString(),
                                IdTrabajador = int.Parse(reader["IdTrabajador"].ToString()),
                                ModalidadPago = reader["ModalidadPago"].ToString(),
                                FechaGenerada = DateTime.Parse(reader["FechaGenerada"].ToString()),
                                FechaLimite = DateTime.Parse(reader["FechaLimite"].ToString()),
                                IdOrden = reader["IdOrden"].ToString(),
                            };
                        }
                        r.Success = true;
                        r.Response = sc;
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
