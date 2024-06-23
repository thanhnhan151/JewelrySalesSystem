using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Gems
{
    public class UpdateGemRequest
    {
        public int GemId { get; set; }
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
