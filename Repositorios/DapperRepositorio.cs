using Dapper;
using Microsoft.Data.SqlClient;

namespace API_VariasBDs.Repositorios
{
    public class DapperRepositorio
    {
        private static readonly string _connectionString;
        static DapperRepositorio()
        {
            _connectionString = GetAppSetting("ConnectionStrings:DefaultConnection");
        }

        public static void Execute(string sql, DynamicParameters? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            connection.Execute(sql, parameters);
            connection.Close();
        }

        public static List<T> Query<T>(string sql, DynamicParameters? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var result = connection.Query<T>(sql, parameters).ToList();
            connection.Close();
            return result;
        }

        public static T SingleOrDefault<T>(string sql, DynamicParameters? parameters = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var result = connection.QuerySingleOrDefault<T>(sql, parameters);
            connection.Close();
            return result;
        }

        private static string GetAppSetting(string key)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration[key] ?? string.Empty;
        }
    }
}
