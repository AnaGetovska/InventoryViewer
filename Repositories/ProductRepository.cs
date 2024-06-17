using Dapper;
using InventoryViewer.Models;
using System.Data.SqlClient;

namespace InventoryViewer.Repositories
{
    public class ProductRepository : IRepository<ProductModel>
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public ProductRepository(IConfiguration config, ILogger<ProductRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void Add(ProductModel newItem)
        {
            var sql = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Query<ProductModel>(sql, new { Name = newItem.Name, Price = newItem.Price });
            }
            _logger.LogTrace("All products were fetched successfully");
        }

        public void Delete(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Query<ProductModel>(sql, new { Id = id });
            }
            _logger.LogTrace($"Product with Id = {id} was deleted.");
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var sql = "USE Inventory SELECT * FROM Products";
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var result = connection.Query<ProductModel>(sql).ToList();
                _logger.LogTrace("All products were fetched successfully");
                return result;
            }
        }

        public ProductModel GetById(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                var result = connection.Query<ProductModel>(sql, new { Id = id }).First();
                _logger.LogTrace($"Product with Id = {id} was fetched successfully");
                return result;
            }
        }

        public void Update(ProductModel updatedRecord)
        {
            var sql = "USE Inventory UPDATE Products SET Name = @Name, Price = @Price, LastModified = @LastModified WHERE Id = @Id";
            using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
            {
                connection.Query<ProductModel>(sql, new {
                    Name = updatedRecord.Name,
                    Price = updatedRecord.Price,
                    LastModified = updatedRecord.LastModified,
                    Id = updatedRecord.Id
                });
                _logger.LogTrace($"Product with Id = {updatedRecord.Id} was updated successfully");
            }
        }
    }
}
