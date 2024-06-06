using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Product;

namespace JewelrySalesSystem.BAL.Models.Colours
{
    public class ColourViewModel
    {
        public int ColourId { get; set; }
        public string ColourName { get; set; } 

        // Products
        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}
