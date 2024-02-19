using Xunit;
using CustomerRecords.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CustomerRecords.Api.Context;
using CustomerRecords.Api.Config;
using Microsoft.EntityFrameworkCore;
using CustomerRecords.Api.Request;
using CustomerRecords.Api.Models;

namespace CustomerRecords.Api.Repository.Tests
{
    public class CustomerRepositoryTests
    {
        private readonly CustomerTransactionContext _context;
        private readonly CustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerRepositoryTests()
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

            _repository = new CustomerRepository(_context, _mapper);

        }

        [Fact]
        public async Task CreateCustomer_Adds_New_Customer()
        {
            // Arrange
            var request = new CreateCustomerRequest
            {
                Name = "John Doe",
                Address = "123 Main St",
                PhoneNumber = 277299772,
            };

            // Act
            await _repository.CreateCustomer(request);

            // Assert
            var savedCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Name == request.Name);
            Assert.NotNull(savedCustomer);
            Assert.Equal(request.Name, savedCustomer.Name);
        }

        [Fact]
        public async Task DeleteCustomer_Removes_Customer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customer = new Customer
            {
                Id = customerId,
                Name = "John Doe",
                Address = "123 Main St",
                PhoneNumber = 288847773889,
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeleteCustomer(customerId);

            // Assert
            var deletedCustomer = await _context.Customers.FindAsync(customerId);
            Assert.Null(deletedCustomer); 
        }

        [Fact]
        public async Task UpdateCustomer_Updates_Customer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var initialName = "Initial Name";
            var updatedName = "Updated Name";
            var customer = new Customer
            {
                Id = customerId,
                Name = initialName,
                Address = "123 Main St",
                PhoneNumber = 2226668888,
                // Add any other properties needed
            };
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            var request = new UpdateCustomerRequest
            {
                Name = updatedName,
                Address = "Updated Address",
                PhoneNumber = 2555777888,
            };

            // Act
            await _repository.UpdateCustomer(request, customerId);

            // Assert
            var updatedCustomer = await _context.Customers.FindAsync(customerId);
            Assert.NotNull(updatedCustomer); 
            Assert.Equal(updatedName, updatedCustomer.Name); 
        }


    }
}