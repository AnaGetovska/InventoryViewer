namespace InventoryViewer.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModified { get; set; }
    }
}
