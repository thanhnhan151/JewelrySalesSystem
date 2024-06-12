using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("GemPriceList")]
    public class GemPriceList
    {
        [Key]
        public int Id { get; set; }
        public float CaratWeightPrice { get; set; }
        public float ClarityPrice { get; set; }
        public float CutPrice { get; set; }
        public float ColourPrice { get; set; }

        // Gem
        public int GemId { get; set; }
        public virtual Gem Gem { get; set; } = null!;
    }
}
