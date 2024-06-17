using InventoryViewer.Models;

namespace InventoryViewer.Services
{
    public interface IProductService
    {
        public IEnumerable<ProductModel> FetchAll();

        public void UpdateRecord(int id, KeyValuePair<string, object> newRecord);
    }
}