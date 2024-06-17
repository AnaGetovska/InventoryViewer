using InventoryViewer.Models;

namespace InventoryViewer.Services
{
    public interface IProductService<T>
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

        /// <summary>
        /// Adds a new record to the specified table in the DB.
        /// </summary>
        /// <param name="newRecord">New record to be added in the database</param>
        public void AddRecord(T newRecord);

        /// <summary>
        /// Deletes a record from the table by given ID.
        /// </summary>
        /// <param name="id">Record ID</param>
        public void DeleteRecord(int id);
    }
}