﻿using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("MaterialPriceList")]
    public class MaterialPriceList
    {
        public int Id { get; set; }
        public float BuyPrice { get; set; } 
        public float SellPrice { get; set; }
        public DateTime EffDate { get; set; }

        // Material
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; } = null!;
    }
}
