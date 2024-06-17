using InventoryViewer.Models;
using InventoryViewer.Repositories;
using InventoryViewer.Utils;

namespace InventoryViewer.Services
{
    public class ProductService : IProductService<ProductModel>
    {
        private readonly ILogger _logger;
        private readonly ProductRepository _repo;
        public ProductService(ILogger<ProductService> logger, ProductRepository repo) {
            _logger = logger;
            _repo = repo;
        }

        public IEnumerable<ProductModel> FetchAll()
        {
            return _repo.GetAll();
        }
      
        public void UpdateRecord(ProductModel updatedData)
        {
            updatedData.LastModified = DateTime.UtcNow;
            _repo.Update(updatedData);
        }

        public void AddRecord(ProductModel newRecord)
        {
            _repo.Add(newRecord);
        }

        public void DeleteRecord(int id)
        {
            _repo.Delete(id);
        }
    }
}
