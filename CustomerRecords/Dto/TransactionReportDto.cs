namespace CustomerRecords.Api.Dto
{
    public class TransactionReportDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<TransactionDto>? Transactions { get; set; }
    }
}
