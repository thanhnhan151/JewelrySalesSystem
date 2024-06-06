using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelrySalesSystem.BAL.Models.Invoices;
using JewelrySalesSystem.BAL.Models.Roles;
using JewelrySalesSystem.DAL.Entities;

namespace JewelrySalesSystem.BAL.Models.Users
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Address { get; set; } 
        public bool Status { get; set; }

        // Role
        public int RoleId { get; set; }
        public virtual RoleViewModel Role { get; set; } 

        // Invoices
        public virtual ICollection<InvoiceViewModel> Invoices { get; set; }
    }
}
