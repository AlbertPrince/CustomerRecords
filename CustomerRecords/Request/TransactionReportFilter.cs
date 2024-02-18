namespace CustomerRecords.Api.Request
{
    public class CreateTransactionReportRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
