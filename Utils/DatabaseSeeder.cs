using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace InventoryViewer.Utils
{
    public class DatabaseSeeder
    {
        public const string DATABASE_NAME = "Inventory";

        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public DatabaseSeeder(IConfiguration config, ILogger<DatabaseSeeder> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void EnsureDatabaseExists()
        {
            try
            {
                CreateDatabase(DATABASE_NAME);
            }
            catch
            {
                _logger.LogDebug("Database already exists.");
            }

            try
            {
                CreateSchema(DATABASE_NAME);
            }
            catch
            {
                _logger.LogDebug("Table already exists.");
            }

            int count = 0;
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var sql = "USE Inventory SELECT COUNT(*) FROM Products";
                count = connection.ExecuteScalar<int>(sql);
            }

            if (count == 0)
            {
                PopulateDatabase();
            }
        }

        private void CreateDatabase(string databaseName)
        {
            var sql = $"CREATE DATABASE {databaseName}";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Execute(sql);
            }
        }

        private void CreateSchema(string databaseName)
        {
            var sql = $"USE {databaseName}\r\nCREATE TABLE Products (\r\n" +
                "Id int NOT NULL IDENTITY(1,1) PRIMARY KEY,\r\n" +
                "Name varchar(255),\r\nPrice decimal(10,2),\r\n" +
                "DateAdded date not null default (GETDATE()),\r\n" +
                "LastModified date not null default (GETDATE()))";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Execute(sql);
            }
        }

        private void PopulateDatabase()
        {
            var sql = "USE Inventory INSERT INTO Products (Name, Price) VALUES (@Name, @Price);";
            object[] parameters = {
                new { Name = "Bananas", Price = 12.99 },
                new { Name = "Potatoes", Price = 2.99 },
                new { Name = "Strawberies", Price = 6.40 },
                new { Name = "Peaches", Price = 2.59 },
                new { Name = "Tomatoes", Price = 1.69 },
                new { Name = "Cucumber", Price = 4.50 },
                new { Name = "Grape", Price = 3.70 },
                new { Name = "Pineapple", Price = 7.20 }
            };

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Execute(sql, parameters);
            }
        }
    }
}
