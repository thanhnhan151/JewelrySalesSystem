using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Materials;
using JewelrySalesSystem.BAL.Models.Product;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.ProductMaterial
{
    public class ProductMaterialViewModel
    {
        public int Id { get; set; }
        public float Weight { get; set; }

        // Product
        public int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }

        // Material
        public int MaterialId { get; set; }
        public virtual MaterialViewModel Material { get; set; } 
    }
}
