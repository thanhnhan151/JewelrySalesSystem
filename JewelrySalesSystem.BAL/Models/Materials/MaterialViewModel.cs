using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.MaterialPriceList;
using JewelrySalesSystem.BAL.Models.ProductMaterial;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Materials
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; } 

        // Product Materials
        public virtual ICollection<ProductMaterialViewModel> ProductMaterials { get; set; } 

        // Material Price List
        public virtual ICollection<MaterialPriceListViewModel> MaterialPrices { get; set; }
    }
}
