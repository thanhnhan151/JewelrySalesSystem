using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Customers
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } 
        public string PhoneNumber { get; set; } 
        public int Point { get; set; }

        // Invoices
        public virtual ICollection<InvoiceViewModel> Invoices { get; set; } 
    }
}
