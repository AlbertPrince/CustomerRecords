using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Request;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRecords.Api.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionsRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionsRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<ActionResult<decimal>> RecordTransaction(CreateTransactionRequest request)
        {
            try
            {
                decimal balance = await _transactionsRepository.RecordTransaction(request);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
