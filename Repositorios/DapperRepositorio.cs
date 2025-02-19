using Dapper;
using Microsoft.Data.SqlClient;

namespace API_VariasBDs.Repositorios
{
    public class DapperRepositorio
    {
        public static void Execute(string sql, DynamicParameters parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(GetAppSetting("ConnectionStrings:DefaultConnection")))
            {
                connection.Open();
                connection.Execute(sql, parameters);
                connection.Close();
            }
        }

        public static List<T> Query<T>(string sql, DynamicParameters parameters = null)
        {
            List<T> result = new List<T>();
            using (SqlConnection connection = new SqlConnection(GetAppSetting("ConnectionStrings:DefaultConnection")))
            {
                connection.Open();
                result = connection.Query<T>(sql, parameters).ToList();
                connection.Close();
            }

            return result;
        }

        public static T SingleOrDefault<T>(string sql, DynamicParameters parameters = null)
        {
            T result = default;

            using (SqlConnection connection = new SqlConnection(GetAppSetting("ConnectionStrings:DefaultConnection")))
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
