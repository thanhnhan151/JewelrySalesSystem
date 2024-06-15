using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.ProductTypes
{
    public class GetProductTypeResponse
    {
        //public int Id { get; set; }
        //public string Name { get; set; } = string.Empty;

        // Products
        public ICollection<ProductAndProductTypeResponse> Products { get; set; } = [];
    }
}
