using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI.Repository
{
    public class Connection
    {
        public SqlConnection sqlConnection;
        private string connectionString;
        public Connection()
        {
            connectionString = GetConfiguration().GetSection("ConnectionStrings:DefaultConnection").Value;
            sqlConnection = new SqlConnection(connectionString);
        }
        IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
