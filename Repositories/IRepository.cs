using InventoryViewer.Models;

namespace InventoryViewer.Repositories
{
    public interface IRepository<T>
    {
        void Add(T newItem);
        void Delete(int Id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(int id, KeyValuePair<string, object> updatedRecord);
    }
}
