using InventoryViewer.Models;
using InventoryViewer.Repositories;

namespace InventoryViewer.Services
{
    public class ProductService : IService<ProductModel>
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
    }
}
