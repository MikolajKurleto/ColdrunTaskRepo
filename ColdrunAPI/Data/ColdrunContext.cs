using ColdrunAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColdrunAPI.Data
{
    public class ColdrunContext : DbContext
    {
        public DbSet<TruckModel>? Trucks { get; set; }
        public DbSet<EmployeeModel>? Employees { get; set; }
        public DbSet<CustomerModel>? Customers { get; set; }
        public DbSet<FactoryModel>? Factories { get; set; }

        public ColdrunContext(DbContextOptions<ColdrunContext> options) 
            : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TruckModel>()
                .HasIndex(e => e.Code)
                .IsUnique();
        }
    }
}
