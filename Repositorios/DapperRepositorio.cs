using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace API_VariasBDs.Repositorios
{
    public class DapperRepositorio
    {
        private static string GetConnectionString(string dbName)
        {
            string connectionString = GetAppSetting($"ConnectionStrings:{dbName}");
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"{dbName} não existe em ConnectionStrings");
            return connectionString;
        }

        private static SqlConnection GetOpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static void Execute(string dbName, string sql, DynamicParameters parameters = null)
        {
            string connectionString = GetConnectionString(dbName);

            using (var connection = GetOpenConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

        public static List<T> Query<T>(string dbName, string sql, DynamicParameters parameters = null)
        {
            string connectionString = GetConnectionString(dbName);

            using (var connection = GetOpenConnection(connectionString))
            {
                return connection.Query<T>(sql, parameters).ToList();
            }
        }

        public static T SingleOrDefault<T>(string dbName, string sql, DynamicParameters parameters = null)
        {
            string connectionString = GetConnectionString(dbName);

            using (var connection = GetOpenConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<T>(sql, parameters);
            }
        }

        public static string GetAppSetting(string value)
        {
            IConfiguration conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return conf[value] ?? "";
        }
    }
}