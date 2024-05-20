using JewelrySalesSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JewelrySalesSystem.DAL.Persistence
{
    public class JewelryDbContext : DbContext
    {
        public JewelryDbContext()
        {

        }


        public JewelryDbContext(DbContextOptions<JewelryDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=1;Database=JewelrySalesSystem;Trusted_Connection=true;TrustServerCertificate=true;");
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
