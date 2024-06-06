using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.GemPriceList;
using JewelrySalesSystem.BAL.Models.ProductGems;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Gems
{
    public class GemViewModel
    {
        public int GemId { get; set; }
        public string GemName { get; set; }
        public string Origin { get; set; } 
        public float CaratWeight { get; set; }
        public string Colour { get; set; } 
        public string Clarity { get; set; } 
        public string Cut { get; set; } 
        public bool Status { get; set; }

        // Gem Price List
        public virtual ICollection<GemPriceListViewModel> GemPrices { get; set; }

        // Product Gems
        public virtual ICollection<ProductGemViewModel> ProductGems { get; set; }
    }
}
