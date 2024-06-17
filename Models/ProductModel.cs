using System.ComponentModel.DataAnnotations;

namespace InventoryViewer.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between $1 and $10 000")]
        public double Price { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModified { get; set; }
    }
}
