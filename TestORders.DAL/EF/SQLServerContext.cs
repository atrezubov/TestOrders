//EF/SQLServerContext.cs.
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestOrders.DAL.Entities;

namespace TestOrders.DAL.EF
{
    /// <summary>
    /// Data context for MS SQL Server.
    /// </summary>
    public class SQLServerContext : DbContext
    {
        public DbSet<Customer> Customers {get; set;}
        public DbSet<Order> Orders { get; set; }

       public SQLServerContext(DbContextOptions<SQLServerContext> options ) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Setting default values for data columns.
            modelBuilder.Entity<Customer>()
                .Property(b => b.Deleted)
                .HasDefaultValue(false);
            modelBuilder.Entity<Order>()
                .Property(b => b.Deleted)
                .HasDefaultValue(false);
            modelBuilder.Entity<Order>()
                .Property(b => b.Date)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Order>()
                .Property(b => b.Description)
                .HasDefaultValue("-");
        }
    }
}
