namespace CustomerRecords.Api.Filter
{
    public class TransactionFilter
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
