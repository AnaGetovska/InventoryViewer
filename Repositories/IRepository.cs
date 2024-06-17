using InventoryViewer.Models;

namespace InventoryViewer.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds a new record to a given database table.
        /// </summary>
        /// <param name="newItem">The new record to be added</param>
        void Add(T newItem);

        /// <summary>
        /// Removes a record from a table by a given ID.
        /// </summary>
        /// <param name="Id">ID corresponding to the record to be deleted.</param>
        void Delete(int Id);

        /// <summary>
        /// Retrieves all records from a specified table in the DB.
        /// </summary>
        /// <returns>All rows as <see cref="IEnumerable{T}"/> from the specified DB table.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieves a record by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedRecord"></param>
        void Update(T updatedRecord);
    }
}
