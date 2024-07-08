﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Counter")]
    public class Counter
    {
        [Key]
        public int CounterId { get; set; }
        public string CounterName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
