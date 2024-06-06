using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Categories;
using JewelrySalesSystem.BAL.Models.Colours;
using JewelrySalesSystem.BAL.Models.Genders;
using JewelrySalesSystem.BAL.Models.InvoiceDetails;
using JewelrySalesSystem.BAL.Models.ProductGems;
using JewelrySalesSystem.BAL.Models.ProductMaterial;
using JewelrySalesSystem.BAL.Models.ProductTypes;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Product
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int PercentPriceRate { get; set; }
        public float ProductionCost { get; set; }
        public bool Status { get; set; }
        public string FeaturedImage { get; set; } = string.Empty;
        // Category
        public int CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; } = null!;

        // Product Type
        public int ProductTypeId { get; set; }
        public virtual ProductTypeViewModel ProductType { get; set; } 

        // Gender
        public int GenderId { get; set; }
        public virtual GenderViewModel Gender { get; set; }

        // Colour
        public int ColourId { get; set; }
        public virtual ColourViewModel Colour { get; set; } 

        // Invoice Details
        public virtual ICollection<InvoiceDetailViewModel> InvoiceDetails { get; set; }

        // Product Gems
        public virtual ICollection<ProductGemViewModel> ProductGems { get; set; }

        // Product Materials
        public virtual ICollection<ProductMaterialViewModel> ProductMaterials { get; set; } 
    }
}
