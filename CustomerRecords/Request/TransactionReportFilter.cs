namespace CustomerRecords.Api.Request
{
    public class TransactionReportFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
