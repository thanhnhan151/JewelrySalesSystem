using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.MaterialPriceList
{
    public class MaterialPriceListViewModel
    {
        public int Id { get; set; }
        public float BuyPrice { get; set; }
        public float SellPrice { get; set; }
        public DateTime EffDate { get; set; }

        // Material
        public int MaterialId { get; set; }
        public virtual MaterialViewModel Material { get; set; } 
    }
}
