using System.Collections.Generic;
using System.Reflection.Emit;
using BO;
using Microsoft.EntityFrameworkCore;

namespace Ex06_EntityFramework.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <Warehouse> warehouse { get; set; }
        public DbSet <Articles> articles { get; set; }
        public DbSet <Orders> orders { get; set; }
        public DbSet <OrderDetails> orderDetails{ get; set; }
        //public DbSet <Customers> customer{ get; set; }

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

            //modelBuilder.Entity<Customers>(entity =>
            //{
            //    // définir la clé primaire
            //    entity.HasKey(c => c.CustomerId);

            //    // définir la relation 1:N avec Orders
            //    entity.HasMany(c => c.Orders);
            //});
        }
    }

}
