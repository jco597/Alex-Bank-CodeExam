﻿using System;
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
                entity.Property(e => e.Id).HasColumnType<Guid>("TEXT");
                entity.Property(e => e.FromAccount).HasColumnType<string>("TEXT");
                entity.Property(e => e.ToAccount).HasColumnType<string>("TEXT");
                entity.Property(e => e.Amount).HasColumnType<decimal>("NUMERIC");
                entity.Property(e => e.Description).HasColumnType<string>("TEXT");
                entity.Property(e => e.Owner);
                entity.Property(e => e.TransactionDate).HasColumnType<DateTime>("TEXT");
            });

            modelBuilder.Entity<Customer>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnType<Guid>("TEXT");
                entity.Property(e => e.Name).HasColumnType<string>("TEXT");
            });
         }

    }
}
