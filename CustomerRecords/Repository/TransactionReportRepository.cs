using AutoMapper;
using CustomerRecords.Api.Context;
using CustomerRecords.Api.Dto;
using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRecords.Api.Repository
{
    public class TransactionReportRepository : ITransactionReportRepository
    {
        private readonly CustomerTransactionContext _context;
        private readonly IMapper _mapper;

        public TransactionReportRepository(CustomerTransactionContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionReportDto> GetTransactionReports(TransactionReportFilter filter)
        {
            var query = _context.Transactions.AsQueryable();

            // Apply filters based on request parameters
            if (filter.CustomerId.HasValue)
            {
                query = query.Where(t => t.CustomerId == filter.CustomerId);
            }

            if (filter.StartDate.HasValue)
            {
                query = query.Where(t => t.Date >= filter.StartDate);
            }

            if (filter.EndDate.HasValue)
            {
                query = query.Where(t => t.Date <= filter.EndDate);
            }

            // Retrieve transactions based on the filtered query
            var transactions = await query.OrderBy(t => t.Date).ToListAsync();

            // Map transactions to DTOs
            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);

            // Create and return the transaction report
            var report = new TransactionReportDto
            {
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Transactions = transactionDtos
            };

            return report;
        }
    }
}
