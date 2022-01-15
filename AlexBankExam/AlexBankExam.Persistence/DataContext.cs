using System;
using Microsoft.EntityFrameworkCore;
using AlexBankExam.Persistence.Domain;

namespace AlexBankExam.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {            
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Transaction>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FromAccount).IsRequired(true);
                entity.Property(e => e.ToAccount).IsRequired(true);
                entity.Property(e => e.Amount).IsRequired(true);
                entity.Property(e => e.Description).IsUnicode(false);
                entity.Property(e => e.Owner).IsRequired(true).IsUnicode(false);
                entity.Property(e => e.TransactionDate).IsRequired(false);
            });

            modelBuilder.Entity<Customer>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired(true);
            });
         }

    }
}
