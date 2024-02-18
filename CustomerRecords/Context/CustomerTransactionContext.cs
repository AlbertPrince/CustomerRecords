using CustomerRecords.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerRecords.Api.Context
{
    public class CustomerTransactionContext: DbContext
    {
        public CustomerTransactionContext(DbContextOptions<CustomerTransactionContext> options) : base(options) 
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionReport> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.TotalAmount)
                .HasColumnType("decimal(18, 2)");
        }
    }
}
