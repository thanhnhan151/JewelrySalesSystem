﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Models.Products
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int PercentPriceRate { get; set; }
        public float ProductionCost { get; set; }
        public bool Status { get; set; }
        public string MaterialType { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;
        // Category
        public int CategoryId { get; set; }
        
        // Product Type
        public int ProductTypeId { get; set; }
        
        // Gender
        public int GenderId { get; set; }
        
        // Colour
        public int ColourId { get; set; }
        

        // Product Gems
        public virtual ICollection<int> Gems { get; set; } = new List<int>();

        // Product Materials
        public virtual ICollection<int> Materials { get; set; } = new List<int>();
    }
}
