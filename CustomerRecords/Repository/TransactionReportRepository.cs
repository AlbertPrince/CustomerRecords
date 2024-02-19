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

            var transactions = await query.OrderBy(t => t.Date).ToListAsync();

            var transactionDtos = _mapper.Map<List<TransactionDto>>(transactions);

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
