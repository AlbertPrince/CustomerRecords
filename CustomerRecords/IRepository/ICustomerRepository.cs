using CustomerRecords.Api.Request;

namespace CustomerRecords.Api.IRepository
{
    public interface ICustomerRepository
    {
        public Task CreateCustomer(CreateCustomerRequest request);
        public Task UpdateCustomer(UpdateCustomerRequest request, Guid customerId);
        public Task DeleteCustomer(Guid customerId);
    }
}
