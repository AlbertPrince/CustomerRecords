using CustomerRecords.Api.Models;

namespace CustomerRecords.Api.Request
{
    public class CreateTransactionRequest
    {
        public string? Remarks { get; set; }
        public Decimal? TotalAmount { get; set; }
        public Boolean? IsInvoice { get; set; }
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
