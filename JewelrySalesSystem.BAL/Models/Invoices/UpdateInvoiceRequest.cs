using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class UpdateInvoiceRequest
    {
        public int InvoiceId { get; set; }
        public bool Status { get; set; }
        public bool InvoiceType { get; set; }

        // Customer
        public string CustomerName { get; set; } = string.Empty;

        // User
        public int UserId { get; set; }

        // Warranty
        public int WarrantyId { get; set; }

        // Invoice Details
        public virtual ICollection<int> InvoiceDetails { get; set; } = new List<int>();
        //public virtual ICollection<int> Products { get; set; } = new List<int>();


    }
}
