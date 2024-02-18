using CustomerRecords.Api.Models;

namespace CustomerRecords.Api.Dto
{
    public class CustomerDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Balance { get; set; } = 0;
        public long? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
