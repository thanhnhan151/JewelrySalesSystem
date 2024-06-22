﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string InvoiceType { get; set; } = "Sale";
        public string OrderStatus { get; set; } = "Pending";
        public string CustomerName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Warranty { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;

        // Order Details
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];  
    }
}