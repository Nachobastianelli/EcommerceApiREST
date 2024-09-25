using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLines> OrderLines { get; set; }
        public DbSet<Valoration> Valorations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Address> Adresses { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Valoration>()
                .HasOne(o => o.User)
                .WithMany(v => v.Valorations)
                .HasForeignKey(o => o.IdUser);

            modelBuilder.Entity<Valoration>()
                .HasOne(o => o.Product)
                .WithMany(v => v.Valorations)
                .HasForeignKey(o => o.IdProduct);

            modelBuilder.Entity<Order>()
                .HasOne(i => i.Invoice)
                .WithOne(o => o.Order)
                .HasForeignKey<Invoice>(i => i.IdOrder);

            modelBuilder.Entity<OrderLines>()
                .HasOne(o => o.Order)
                .WithMany(ol => ol.OrderLines)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderLines>()
                .HasOne(p => p.Product)
                .WithMany(o => o.OrderLines)
                .HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<Invoice>()
                .HasOne(u => u.User)
                .WithMany(i => i.Invoices)
                .HasForeignKey(i => i.IdUser);

            modelBuilder.Entity<Order>()
                .HasOne(u => u.User)
                .WithMany(i => i.Orders)
                .HasForeignKey(i => i.IdUser);

            modelBuilder.Entity<Order>()
                .HasOne(a => a.Address)
                .WithOne(o => o.Order)
                .HasForeignKey<Address>(a => a.OrderId);

        }


    }
}
