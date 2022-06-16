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
    public class GuiaRemisionRepository: IGuiaRemisionRepository
    {
        private Connection _bd;
        public Result<GuiaRemision> obtenerNuevoNroGuiaRemision(string prefijo)
        {
            _bd = new Connection();
            Result<GuiaRemision> r = new Result<GuiaRemision>();
            GuiaRemision o = new GuiaRemision();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerNuevoNroGuiaRemision, con);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@prefijo", SqlDbType.NVarChar).Value = prefijo;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            o.IdGuiaRemision = reader["IdGuiaRemision"].ToString();
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
        public Result<int> registrarGuiaRemision(GuiaRemision guia)
        {
            _bd = new Connection();
            Result<int> r = new Result<int>();
            int resGuia = 0;
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.RegistrarNuevaGuiaRemision, con);
                    sqlCommand.Parameters.Add("@idguiaremision", SqlDbType.NVarChar).Value = guia.IdGuiaRemision;
                    sqlCommand.Parameters.Add("@idordenpedido", SqlDbType.NVarChar).Value = guia.IdOrdenPedido;
                    sqlCommand.Parameters.Add("@idtransportista", SqlDbType.NVarChar).Value = guia.IdTransportista;
                    sqlCommand.Parameters.Add("@vehiculo", SqlDbType.NVarChar).Value = guia.Vehiculo;
                    sqlCommand.Parameters.Add("@placa", SqlDbType.NVarChar).Value = guia.Placa;
                    sqlCommand.Parameters.Add("@modelo", SqlDbType.NVarChar).Value = guia.Modelo;
                    sqlCommand.Connection = con;
                    con.Open();

                    resGuia = sqlCommand.ExecuteNonQuery();


                    r.Success = true;
                    r.Response = resGuia;
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
