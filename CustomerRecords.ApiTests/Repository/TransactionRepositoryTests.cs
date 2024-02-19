using Xunit;
using CustomerRecords.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using CustomerRecords.Api.Models;
using CustomerRecords.Api.Context;
using Microsoft.EntityFrameworkCore;
using CustomerRecords.Api.Request;
using CustomerRecords.Api.Config;

namespace CustomerRecords.Api.Repository.Tests
{
    public class TransactionRepositoryTests : IDisposable
    {
        private readonly CustomerTransactionContext _context;
        private readonly TransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionRepositoryTests()
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

            _repository = new TransactionRepository(_mapper, _context);
        }

        [Fact]
        public async Task RecordTransaction_ValidRequest_ReturnsCustomerBalance()
        {

            // Arrange
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                Balance = 0 
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();


            var request = new CreateTransactionRequest
            {
                CustomerId = customerId,
                Amount = 100,
                IsInvoice = true
            };

            // Act
            var balance = await _repository.RecordTransaction(request);

            // Assert
            Assert.Equal(100, balance); 
        }

        [Fact]
        public async Task RecordTransaction_ValidRequest_ReturnsCustomerBalance_For_Payment()
        {

            // Arrange
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                Balance = 100
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();


            var request = new CreateTransactionRequest
            {
                CustomerId = customerId,
                Amount = 50,
                IsInvoice = false
            };

            // Act
            var balance = await _repository.RecordTransaction(request);

            // Assert
            Assert.Equal(50, balance);
        }

        [Fact]
        public async Task RecordTransaction_ValidRequest_Returns_Exception_For_Amount_Equals_0()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                Balance = 100
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var request = new CreateTransactionRequest
            {
                CustomerId = customerId,
                Amount = 0,
                IsInvoice = false
            };

            // Act
            Exception exception = await Record.ExceptionAsync(() => _repository.RecordTransaction(request));

            // Assert
            Assert.NotNull(exception);

            Assert.IsType<Exception>(exception);
            Assert.Equal("The transaction amount must be greater than 0", exception.Message);
        }

        [Fact]
        public async Task RecordTransaction_ValidRequest_Returns_Exception_For_Amount_Greater_Than_Balance()
        {
            // Arrange
            var customerId = Guid.NewGuid();

            var customer = new Customer
            {
                Id = customerId,
                Balance = 100
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var request = new CreateTransactionRequest
            {
                CustomerId = customerId,
                Amount = 460,
                IsInvoice = false
            };

            // Act
            Exception exception = await Record.ExceptionAsync(() => _repository.RecordTransaction(request));

            // Assert
            Assert.NotNull(exception);

            Assert.IsType<Exception>(exception);
            Assert.Equal("The transaction amount can't be greater than the customer's balance for payments", exception.Message);
        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
