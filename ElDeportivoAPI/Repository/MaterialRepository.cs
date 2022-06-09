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
    public class MaterialRepository: IMaterialRepository
    {
        private Connection _bd;
        public Result<List<Material>> obtenerMaterialDeficit(int idCategoria)
        {
            _bd = new Connection();
            List<Material> lista = new List<Material>();
            Result<List<Material>> r = new Result<List<Material>>();
            using (SqlConnection con = _bd.sqlConnection)
            {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Queries.ObtenerMaterialesDeficit, con);
                    sqlCommand.Parameters.Add("@idCategoria", SqlDbType.Int).Value = idCategoria;
                    sqlCommand.Connection = con;
                    con.Open();

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Material
                            {
                                CodigoMaterial = reader["CodigoMaterial"].ToString(),
                                Nombre = reader["Nombre"].ToString(),
                                Descripcion = reader["Descripcion"].ToString(),
                                Marca = reader["Marca"].ToString(),
                                Presentacion = reader["Presentacion"].ToString(),
                                Stock = double.Parse(reader["Stock"].ToString()),
                                Limite = double.Parse(reader["Limite"].ToString()),
                                CantidadMinima = double.Parse(reader["CantidadMinima"].ToString()),
                                Precio = double.Parse(reader["Precio"].ToString()),
                                IdCategoria = int.Parse(reader["IdCategoria"].ToString()),
                                Categoria = reader["Categoria"].ToString()
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
