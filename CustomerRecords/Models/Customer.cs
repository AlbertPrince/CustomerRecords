namespace CustomerRecords.Api.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Balance { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
