namespace CustomerRecords.Api.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set;}
        public string? Remarks { get; set; }
        public Decimal? TotalAmount { get; set; }
        public Boolean? IsInvoice { get; set; }
        public Guid CustomerId { get; set; } 
        public Customer? Customer { get; set; }

    }
}
