using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem.DAL.Entities
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; } = string.Empty;
        public virtual IEnumerable<User>? Users { get; set; }
    }
}
