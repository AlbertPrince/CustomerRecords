namespace CustomerRecords.Api.Models
{
    public class TransactionReport
    {
        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Transaction>? Transactions { get; set; }
    }
}
