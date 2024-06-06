using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Warranties
{
    public class WarrantyViewModel
    {
        public int WarrantyId { get; set; }
        public string Description { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Invoices
        public virtual ICollection<InvoiceViewModel> Invoices { get; set; } 
    }
}
