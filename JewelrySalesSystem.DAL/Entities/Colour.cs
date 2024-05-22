using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Colour")]
    public class Colour
    {
        [Key]
        public int ColourId { get; set; }
        public string ColourName { get; set; } = string.Empty;

        // Products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
