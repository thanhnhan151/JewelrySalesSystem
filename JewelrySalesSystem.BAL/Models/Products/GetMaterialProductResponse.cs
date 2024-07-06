﻿using JewelrySalesSystem.BAL.Models.Materials;

namespace JewelrySalesSystem.BAL.Models.Products
{
    public class GetMaterialProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public MaterialPrice MaterialPrice { get; set; } = null!;

        public bool IsActive { get; set; }
        // Product Type
        public string ProductType { get; set; } = string.Empty;
    }
}
