namespace CustomerRecords.Api.Models
{
    public class Transaction
    {
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set;}
        public Guid TransactionId { get; set; }
        public string Remarks { get; set; }
        public Decimal? TotalAmount { get; set; }
        public Boolean? IsInvoice { get; set; }
        public int CustomerId { get; set; } 
        public Customer Customer { get; set; }
    }
}
