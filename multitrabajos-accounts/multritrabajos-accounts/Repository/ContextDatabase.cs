﻿using Microsoft.EntityFrameworkCore;

namespace multritrabajos_accounts.Repository
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options) { }
        public DbSet<Models.Account> Account { get; set; }
        public DbSet<Models.Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Account>().ToTable("Account");
            modelBuilder.Entity<Models.Customer>().ToTable("Customer");
        }
    }
}
