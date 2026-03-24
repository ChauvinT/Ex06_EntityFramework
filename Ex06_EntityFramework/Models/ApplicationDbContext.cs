using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Ex06_EntityFramework.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <Warehouse> warehouse { get; set; }
        public DbSet <Articles> articles { get; set; }
        public DbSet <Orders> orders { get; set; }
        public DbSet <OrderDetails> orderDetails{ get; set; }
        public DbSet <Customers> customer{ get; set; }


        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Ex06_EntityFramework;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Orders>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Article)
                .WithMany()
                .HasForeignKey(od => od.ArticleId);

            modelBuilder.Entity<Warehouse>()
                .Property(e => e.CodeAccesMD5)
                .HasConversion(v => string.Join(";", v),
                v => v.Split(new[] { ';' }, StringSplitOptions.None).ToList());

            modelBuilder.Entity<Customers>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);          
            
            // Configurer le Value Object Address
            modelBuilder.Entity<Customers>()
                .OwnsOne(c => c.Address, a =>
                {
                    a.Property(p => p.Street).HasColumnName("Address_Street");
                    a.Property(p => p.City).HasColumnName("Address_City");
                    a.Property(p => p.PostalCode).HasColumnName("Address_PostalCode");
                    a.Property(p => p.State).HasColumnName("Address_State");
                    a.Property(p => p.Country).HasColumnName("Address_Country");
                });
        }
    }

}
