﻿namespace JewelrySalesSystem.BAL.Models.Orders
{
    public class OrderItem
    {
        public string ProductName { get; set; } = string.Empty;
        public float SellPrice { get; set; }
        public float BuyPrice { get; set; }
        public float PerDiscount { get; set; }
    }
}
