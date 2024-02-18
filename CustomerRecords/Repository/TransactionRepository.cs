using AutoMapper;
using CustomerRecords.Api.Context;
using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Models;
using CustomerRecords.Api.Request;
using Microsoft.EntityFrameworkCore;

namespace CustomerRecords.Api.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CustomerTransactionContext _context;
        private readonly IMapper _mapper;
        public TransactionRepository(IMapper mapper, CustomerTransactionContext context) 
        {
            _mapper = mapper;
            _context = context;
        }
        

        public async Task<decimal> RecordTransaction(CreateTransactionRequest request)
        {
            var transaction = _mapper.Map<Transaction>(request);
            _context.Transactions.Add(transaction);


            await _context.SaveChangesAsync();

            var customerTransactions = await _context.Transactions
                .Where(t => t.CustomerId == transaction.CustomerId)
                .OrderBy(t => t.Date)
                .ToListAsync();

            decimal balance = 0;
            foreach (var trans in customerTransactions)
            {
                if (trans.IsInvoice)
                {
                    balance += trans.Amount;
                }
                else
                {
                    balance -= trans.Amount;
                }
            }

            return balance;
        }

    }
}
