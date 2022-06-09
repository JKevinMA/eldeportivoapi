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
    public class ProveedorRepository: IProveedorRepository
    {
        private Connection _bd;

        public Result<List<Proveedor>> buscarProveedores(string campo,string valor)
        {
            _bd = new Connection();
            List<Proveedor> lista = new List<Proveedor>();
            Result<List<Proveedor>> r = new Result<List<Proveedor>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    string fullQuery = Queries.BuscarProveedor+" Where " +campo+ " like '%"+valor+"%'";
                    SqlCommand sqlCommand = new SqlCommand(fullQuery, con);
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                Ruc = reader["Ruc"].ToString(),
                                RazonSocial = reader["RazonSocial"].ToString(),
                                Telefono = reader["Telefono"].ToString()
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
        public Result<List<Proveedor>> obtenerProveedoresCotizacion(string idSolicitudCotizacion)
        {
            _bd = new Connection();
            List<Proveedor> lista = new List<Proveedor>();
            Result<List<Proveedor>> r = new Result<List<Proveedor>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerProveedoresCotizacion, con);
                    sqlCommand.Parameters.Add("@idSolicitudCotizacion", SqlDbType.NVarChar).Value = idSolicitudCotizacion;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                Ruc = reader["Ruc"].ToString(),
                                RazonSocial = reader["RazonSocial"].ToString(),
                                Telefono = reader["Telefono"].ToString(),
                                Direccion = reader["Direccion"].ToString()
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
