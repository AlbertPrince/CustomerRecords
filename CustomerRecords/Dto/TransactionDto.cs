namespace CustomerRecords.Api.Dto
{
    public class TransactionDto
    {
        public DateTime Date { get; set; }
        public string? Remarks { get; set; }
        public Decimal Amount { get; set; }
        public Boolean IsInvoice { get; set; }
    }
}
