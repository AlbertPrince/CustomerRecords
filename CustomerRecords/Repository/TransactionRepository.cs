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
            var customer = await _context.Customers.FindAsync(transaction.CustomerId) ?? throw new Exception("Customer not found");

            if (request.Amount == 0)
            {
                throw new Exception("The transaction amount must be greater than 0");
            }

            if (transaction.IsInvoice)
            {
                customer.Balance += transaction.Amount;
            }
            else
            {
                if (request.Amount > customer.Balance)
                {
                    throw new Exception("The transaction amount can't be greater than the customer's balance for payments");
                }
                customer.Balance -= transaction.Amount;
            }

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return customer.Balance;
        }



    }
}
