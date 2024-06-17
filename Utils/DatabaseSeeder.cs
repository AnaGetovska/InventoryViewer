using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace InventoryViewer.Utils
{
    public class DatabaseSeeder
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly string _dbName = "Inventory";

        public DatabaseSeeder(IConfiguration config, ILogger<DatabaseSeeder> logger)
        {
            _config = config;
            _logger = logger;
            _dbName = _config.GetRequiredSection("DB:Name")?.Value ?? "Inventory";
        }

        public void EnsureDatabaseExists()
        {
            try
            {
                CreateDatabase(_dbName);
            }
            catch
            {
                _logger.LogDebug("Database already exists.");
            }

            try
            {
                CreateSchema(_dbName);
            }
            catch
            {
                _logger.LogDebug("Table already exists.");
            }

            int count = 0;
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var sql = $"USE {_dbName} SELECT COUNT(*) FROM Products";
                count = connection.ExecuteScalar<int>(sql);
            }

            if (count == 0)
            {
                PopulateDatabase(_dbName);
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
                "LastModified date not null default (GETDATE()-1))";

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Execute(sql);
            }
        }

        private void PopulateDatabase(string databaseName)
        {
            var sql = $"USE {databaseName} INSERT INTO Products (Name, Price, DateAdded) VALUES (@Name, @Price, @DateAdded);";
            object[] parameters = {
                new { Name = "Bananas", Price = 12.99, DateAdded = RandomDay() },
                new { Name = "Potatoes", Price = 2.99, DateAdded = RandomDay()  },
                new { Name = "Strawberies", Price = 6.40, DateAdded = RandomDay()  },
                new { Name = "Peaches", Price = 2.59, DateAdded = RandomDay()  },
                new { Name = "Tomatoes", Price = 1.69, DateAdded = RandomDay()  },
                new { Name = "Cucumbers", Price = 4.50, DateAdded = RandomDay() },
                new { Name = "Grapes", Price = 3.70, DateAdded = RandomDay() },
                new { Name = "Pineapples", Price = 7.20, DateAdded = RandomDay() }
            };

            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Execute(sql, parameters);
            }
        }

        private DateTime RandomDay()
        {
            Random random = new Random();
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
