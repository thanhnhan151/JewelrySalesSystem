using JewelrySalesSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JewelrySalesSystem.DAL.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
