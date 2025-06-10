
using System.Configuration;
using System.Data.SqlClient;


namespace RedMenTeWeb.Data
{
    public class ConexionDB
    {
        private readonly string _connectionString;

        public ConexionDB()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ProyectoDbConnection"].ConnectionString;
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}