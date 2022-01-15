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

            modelBuilder.Entity<Customer>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType<Guid>("uniqueidentifier");
                entity.Property(e => e.Name).HasColumnType<string>("varchar(150)");
            });

            modelBuilder.Entity<Transaction>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType<Guid>("uniqueidentifier");
                entity.Property(e => e.FromAccount).HasColumnType<string>("nvarchar(20)");
                entity.Property(e => e.ToAccount).HasColumnType<string>("nvarchar(20)");
                entity.Property(e => e.Amount).HasColumnType<decimal>("decimal").HasPrecision(18,2);
                entity.Property(e => e.Description).HasColumnType<string>("varchar(250)");
                entity.Property(e => e.TransactionDate).HasColumnType("datetime2");
                //entity.Property(e => e.Owner).IsRequired();
            });

            modelBuilder.Entity<Customer>().HasMany(x => x.Transactions).WithOne(y => y.Owner);
            modelBuilder.Entity<Transaction>().HasOne(x => x.Owner).WithMany(y => y.Transactions);           
        }

    }
}
