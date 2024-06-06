using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Customers;
using JewelrySalesSystem.BAL.Models.InvoiceDetails;
using JewelrySalesSystem.BAL.Models.Users;
using JewelrySalesSystem.BAL.Models.Warranties;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public int CustomerId { get; set; }
        public virtual CustomerViewModel Customer { get; set; } 

        // User
        public int UserId { get; set; }
        public virtual UserViewModel User { get; set; } 

        // Warranty
        public int WarrantyId { get; set; }
        public virtual WarrantyViewModel Warranty { get; set; } 

        // Invoice Details
        public virtual ICollection<InvoiceDetailViewModel> InvoiceDetails { get; set; } 
    }
}
