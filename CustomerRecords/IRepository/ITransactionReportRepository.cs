using CustomerRecords.Api.Dto;
using CustomerRecords.Api.Request;

namespace CustomerRecords.Api.IRepository
{
    public interface ITransactionReportRepository
    {
        public Task<TransactionReportDto> GetTransactionReports(CreateTransactionReportRequest request);
    }
}
