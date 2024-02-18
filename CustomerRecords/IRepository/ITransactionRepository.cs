using CustomerRecords.Api.Request;

namespace CustomerRecords.Api.IRepository
{
    public interface ITransactionRepository
    {
        public Task<decimal> RecordTransaction(CreateTransactionRequest request);
        
    }
}
