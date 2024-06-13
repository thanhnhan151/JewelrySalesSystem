using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Models.Categories
{
    public class UpdateCategory
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public bool Status { get; set; }
    }
}
