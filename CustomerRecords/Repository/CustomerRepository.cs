using AutoMapper;
using CustomerRecords.Api.Context;
using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Models;
using CustomerRecords.Api.Request;

namespace CustomerRecords.Api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerTransactionContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(CustomerTransactionContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateCustomer(CreateCustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId) ?? throw new Exception("Customer not found");
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
        }

        public async Task UpdateCustomer(UpdateCustomerRequest request, Guid customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId) ?? throw new Exception("Customer not found");

            if (customer != null)
            {
                customer.Address = request.Address;
                customer.Name = request.Name;
                customer.PhoneNumber = request.PhoneNumber;
                customer.Description = request.Description;
                customer.Balance = request.Balance;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
