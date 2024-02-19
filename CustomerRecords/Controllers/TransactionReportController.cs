using CustomerRecords.Api.Dto;
using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Request;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerRecords.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionReportController : ControllerBase
    {
        private readonly ITransactionReportRepository _transactionReportRepository;

        public TransactionReportController(ITransactionReportRepository transactionReportRepository)
        {
            _transactionReportRepository = transactionReportRepository;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<TransactionReportDto>> GenerateTransactionReport([FromQuery] TransactionReportFilter request)
        {
            var transactionReport = await _transactionReportRepository.GetTransactionReports(request);
            return Ok(transactionReport);
        }
    }
}
