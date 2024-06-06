using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Models.ProductTypes
{
    public class ProductTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Products
        public virtual ICollection<ProductTypeViewModel> Products { get; set; } 
    }
}
