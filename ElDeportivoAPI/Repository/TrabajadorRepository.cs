using ElDeportivoAPI.Models;
using ElDeportivoAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository
{
    public class TrabajadorRepository: ITrabajadorRepository
    {
        private Connection _bd;
        public Result<Trabajador> Login(Trabajador t)
        {
            _bd = new Connection();
            Trabajador usuario = null;
            Result<Trabajador> r = new Result<Trabajador>();

            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.Login, con);
                    sqlCommand.Parameters.AddWithValue("@usuario", t.Usuario);
                    sqlCommand.Parameters.AddWithValue("@contrasena", t.Contrasena);
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuario = new Trabajador()
                            {
                                IdTrabajador = int.Parse(reader["IdTrabajador"].ToString()),
                                Dni = int.Parse(reader["Dni"].ToString()),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Usuario = reader["Usuario"].ToString(),
                                Contrasena = reader["Contrasena"].ToString(),
                                IdRol = int.Parse(reader["IdRol"].ToString()),
                                Rol = reader["Rol"].ToString(),
                                IdArea = int.Parse(reader["IdArea"].ToString()),
                                Area = reader["Area"].ToString()
                            };
                        }
                        r.Success = true;
                        r.Response = usuario;
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
