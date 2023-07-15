using Rent_Somethings.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Rent_Somethings.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District_Product>().HasKey(am => new
            {
                am.DistrictId,
                am.ProductId
            });
            modelBuilder.Entity<District_Product>().HasOne(m => m.Product).WithMany(am=> am.Districts_Products).HasForeignKey(m => m.ProductId);
            modelBuilder.Entity<District_Product>().HasOne(m => m.District).WithMany(am => am.Districts_Products).HasForeignKey(m => m.DistrictId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<District> Districts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<District_Product> Districts_Products { get; set; }
        public DbSet<City> Cities { get; set; }
        

        //Rental related tables

        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalItem> RentalsItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}
    }
}
