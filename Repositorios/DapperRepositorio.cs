using Dapper;
using Microsoft.Data.SqlClient;
using System.Xml.Linq;

namespace API_VariasBDs.Repositorios
{
    public class DapperRepositorio
    {
        public static void Execute(string dbName, string sql, DynamicParameters parameters = null)
        {
            string connectionString = GetAppSetting($"ConnectionStrings:{dbName}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(sql, parameters);
                connection.Close();
            }
        }

        public static List<T> Query<T>(string dbName, string sql, DynamicParameters parameters = null)
        {
            List<T> result = new List<T>();

            // Obtém a connection string do appsettings.json
            string connectionString = GetAppSetting($"ConnectionStrings:{dbName}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.Query<T>(sql, parameters).ToList();
                connection.Close();
            }

            return result;
        }

        public static T SingleOrDefault<T>(string dbName, string sql, DynamicParameters parameters = null)
        {
            T result = default;
            string connectionString = GetAppSetting($"ConnectionStrings:{dbName}");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                result = connection.QuerySingleOrDefault<T>(sql, parameters);
                connection.Close();
            }

            return result;
        }

        public static string GetAppSetting(string value)
        {
            IConfiguration conf = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return (conf[value] ?? "");
        }
    }
}
