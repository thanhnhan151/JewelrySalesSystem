using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Models.Product;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.InvoiceDetails
{
    public class InvoiceDetailViewModel
    {
        public int Id { get; set; }
        public float ProductPrice { get; set; }

        // Product
        public int ProductId { get; set; }
        public virtual ProductViewModel Product { get; set; }

        // Invoice
        public int InvoiceId { get; set; }
        public virtual InvoiceViewModel Invoice { get; set; } 
    }
}
