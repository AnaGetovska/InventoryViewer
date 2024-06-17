using InventoryViewer.Models;
using InventoryViewer.Repositories;

namespace InventoryViewer.Services
{
    public class ProductService : IProductService
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

        public void UpdateRecord(int productId, KeyValuePair<string, object> newRecord)
        {
            _repo.Update(productId, newRecord);
        }
    }
}
