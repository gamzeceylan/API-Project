using Albaraka.API.Data;
using Albaraka.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Albaraka.API.Controllers
{
    // Ok(200), NotFound(404), NotContent(204), Created(201), BadRequest(400), ServerError(500)

    [ApiController]
    [Route("api/[controller]")]
    public class ABCBankController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public ABCBankController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result= await _customerRepository.GetAllAsync();
            return Ok(result);

        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =  await _customerRepository.GetByIdAsync(id);
            if(result == null)
            {
                return NotFound(id);
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(Customer customer)
        {
            // önceden öyle bir kimlik nuraması veya müşteri no kaydedilmiş mi 
            var data = await _customerRepository.GetByFilter(x => x.IdentityNumber == customer.IdentityNumber || x.CustomerNumber == customer.CustomerNumber);

            if (data == null && customer.CustomerNumber.Length ==16 &&  customer.PhoneNumber.Length == 10 && customer.IdentityNumber.Length == 11)
            {
                var addedCustomer = await _customerRepository.CreateAsync(customer);
                return Created(string.Empty, addedCustomer);
            }
            return BadRequest();
  
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Customer customer)
        {
            var checkCustomer = await _customerRepository.GetByIdAsync(customer.Id);
            if(checkCustomer == null)
            {
                return NotFound(customer.Id);
            }
            if (customer.CustomerNumber.Length == 16 && customer.PhoneNumber.Length == 10 && customer.IdentityNumber.Length == 11)
            {
                await _customerRepository.UpdateAsync(customer);
                return NoContent();
            }
            return BadRequest(); 
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var checkCustomer = await _customerRepository.GetByIdAsync(id);
            if (checkCustomer == null)
            {
                return NotFound(id);
            }
            await _customerRepository.RemoveAsync(id);
            return NoContent();
        }
    }
}
