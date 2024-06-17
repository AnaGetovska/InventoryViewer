using InventoryViewer.Models;

namespace InventoryViewer.Services
{
    public interface IService<T>
    {
        /// <summary>
        /// Retrieves all records from a specified table in the DB.
        /// </summary>
        /// <returns>All rows as <see cref="IEnumerable{T}"/> from the specified DB table.</returns>
        public IEnumerable<T> FetchAll();

        /// <summary>
        /// Updates the given record in the specified table.
        /// </summary>
        /// <param name="newRecord">Record to be modified in the database</param>
        public void UpdateRecord(T newRecord);
    }
}