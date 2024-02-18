using AutoMapper;
using CustomerRecords.Api.Context;
using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Models;
using CustomerRecords.Api.Request;
using Microsoft.EntityFrameworkCore;

namespace CustomerRecords.Api.Repository
{
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly CustomerTransactionContext _context;
        private readonly IMapper _mapper;
        public TransactionRepository(IMapper mapper, CustomerTransactionContext context) 
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<decimal> GetCustomerBalance(Guid customerId)
        {
            var customerTransactions = await _context.Transactions
            .Where(t => t.CustomerId == customerId)
            .OrderBy(t => t.Date)
            .ToListAsync();

            decimal balance = 0;
            foreach (var trans in customerTransactions)
            {
                balance += trans.Amount;
            }

            return balance;
        }

        public async Task RecordTransaction(CreateTransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            await _context.SaveChangesAsync();

            if (transaction.IsInvoice)
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == transaction.CustomerId);
                if (customer != null)
                {
                    customer.Balance += transaction.Amount;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == transaction.CustomerId);
                if (customer != null)
                {
                    customer.Balance -= transaction.Amount;
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
