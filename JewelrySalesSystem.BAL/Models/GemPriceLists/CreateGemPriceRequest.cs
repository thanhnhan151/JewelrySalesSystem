﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.GemPriceLists
{
    public class CreateGemPriceRequest
    {
        //public int Id { get; set; }
        public float CaratWeightPrice { get; set; }
        public float ClarityPrice { get; set; }
        public float CutPrice { get; set; }
        public float ColourPrice { get; set; }
        //public DateTime EffDate { get; set; }

        // Gem
        public int GemId { get; set; }
        //public virtual Gem Gem { get; set; } = null!;
    }
}