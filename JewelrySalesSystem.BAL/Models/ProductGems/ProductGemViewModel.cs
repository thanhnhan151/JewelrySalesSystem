using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Gems;
using JewelrySalesSystem.BAL.Models.Product;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.ProductGems
{
    public class ProductGemViewModel
    {
        public int Id { get; set; }

        // Product
        public int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; } 

        // Gem
        public int GemId { get; set; }
        public virtual GemViewModel Gem { get; set; } 
    }
}
