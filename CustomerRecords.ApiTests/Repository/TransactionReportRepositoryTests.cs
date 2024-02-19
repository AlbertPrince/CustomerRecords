using Xunit;
using Moq;
using AutoMapper;
using CustomerRecords.Api.Context;
using CustomerRecords.Api.Dto;
using CustomerRecords.Api.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRecords.Api.Request;
using CustomerRecords.Api.Models;
using CustomerRecords.Api.Config;

namespace CustomerRecords.Api.Repository.Tests
{
    public class TransactionReportRepositoryTests
    {
        private readonly TransactionReportRepository _repository;
        private readonly CustomerTransactionContext _context;
        private readonly IMapper _mapper;

        public TransactionReportRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CustomerTransactionContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _context = new CustomerTransactionContext(options);

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperConfig());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            _repository = new TransactionReportRepository(_context, _mapper);
        }

        [Fact]
        public async Task GetTransactionReports_Returns_Transactions_With_Filter()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer
            {
                Id = customerId,
                Name = "John Doe",
                Balance = 200
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var startDate = new DateTime(2023, 1, 1);
            var endDate = new DateTime(2023, 1, 31);

            var transactions = new List<Transaction>
            {
                new Transaction { Customer = customer, Date = startDate.AddDays(-1), Amount = 100, IsInvoice = true },
                new Transaction { Customer = customer, Date = startDate, Amount = 200, IsInvoice = false},
                new Transaction { Customer = customer, Date = endDate, Amount = 150 , IsInvoice = true},
            };

            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            var filter = new TransactionReportFilter
            {
                CustomerId = customerId,
                StartDate = startDate,
                EndDate = endDate
            };

            // Act
            var report = await _repository.GetTransactionReports(filter);

            // Assert
            Assert.NotNull(report);
            Assert.Equal(startDate, report.StartDate);
            Assert.Equal(endDate, report.EndDate);
            
        }

    }
}
