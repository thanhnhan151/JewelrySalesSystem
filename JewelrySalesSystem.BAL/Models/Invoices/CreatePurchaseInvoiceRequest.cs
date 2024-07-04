﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Invoices
{
    public class CreatePurchaseInvoiceRequest
    {
        
        public string InvoiceType { get; set; } = string.Empty;
        // Customer
        public string CustomerName { get; set; } = string.Empty;

        // User
        public int UserId { get; set; }

        //public float Weight { get; set; }

        public virtual ICollection<float> Weights {  get; set; } = new List<float>();
        // Invoice Details
        public virtual ICollection<int> InvoiceDetails { get; set; } = new List<int>();
    }
}
