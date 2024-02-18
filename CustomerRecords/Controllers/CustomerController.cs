using CustomerRecords.Api.IRepository;
using CustomerRecords.Api.Request;
using Microsoft.AspNetCore.Mvc;

namespace CustomerRecords.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                await _customerRepository.CreateCustomer(request);
                return Ok("Customer created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(Guid customerId)
        {
            try
            {
                await _customerRepository.DeleteCustomer(customerId);
                return Ok("Customer deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, UpdateCustomerRequest request)
        {
            try
            {
                await _customerRepository.UpdateCustomer(request, customerId);
                return Ok("Customer updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
