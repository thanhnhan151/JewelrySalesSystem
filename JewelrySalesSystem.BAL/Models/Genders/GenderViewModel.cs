using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Product;

namespace JewelrySalesSystem.BAL.Models.Genders
{
    public class GenderViewModel
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; } 

        // Products
        public virtual ICollection<ProductViewModel> Products { get; set; } 
    }
}
