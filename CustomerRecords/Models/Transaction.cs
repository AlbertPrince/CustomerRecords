namespace CustomerRecords.Api.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Remarks { get; set; }
        public Decimal Amount { get; set; }
        public Boolean IsInvoice { get; set; }
        public Guid CustomerId { get; set; } 
        public required Customer Customer { get; set; }

    }
}
