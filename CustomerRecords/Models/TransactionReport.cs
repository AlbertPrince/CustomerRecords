namespace CustomerRecords.Api.Models
{
    public class TransactionReport
    {
        public Guid Id { get; set; }
        public Decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
