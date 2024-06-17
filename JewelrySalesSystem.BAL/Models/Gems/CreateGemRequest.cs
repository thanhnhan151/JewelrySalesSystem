﻿namespace JewelrySalesSystem.BAL.Models.Gems
{
    public class CreateGemRequest
    {
        public string GemName { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public float CaratWeight { get; set; }
        public string Colour { get; set; } = string.Empty;
        public string Clarity { get; set; } = string.Empty;
        public string Cut { get; set; } = string.Empty;
        public GemPrice GemPrice { get; set; } = null!;
    }
}
