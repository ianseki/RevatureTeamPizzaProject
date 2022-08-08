using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_Server.Data;
using Project2_Server.Model;




namespace Project2_Server.API.Controllers
{
    [Route("api/SQL_Customer")]
    [ApiController]
    public class SQL_CustomerController : ControllerBase
    {
        
        
        private readonly SQL_Customer _repo;

        public SQL_CustomerController(SQL_Customer repo) => _repo = repo;

        [HttpGet]
        public async Task<IEnumerable<DMODEL_Customer>> Get()
            => await _repo.DMODEL_Customer.ToListAsync();

        [HttpGet("customer_id")]
        [ProducesResponseType(typeof(DMODEL_Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int customer_id)
        {
            var Customer = await _repo.DMODEL_Customer.FindAsync(customer_id);
            return Customer == null ? NotFound() : Ok(Customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(DMODEL_Customer Customer)
        {
            await _repo.DMODEL_Customer.AddAsync(Customer);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Customer.customer_id }, Customer);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int customer_id, DMODEL_Customer Customer)
        {
            if (customer_id != Customer.customer_id) return BadRequest();

            _repo.Entry(Customer).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int customer_id)
        {
            var CustomerToDelete = await _repo.DMODEL_Customer.FindAsync(customer_id);
            if (CustomerToDelete == null) return NotFound();

            _repo.DMODEL_Customer.Remove(CustomerToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }





        




    }
}
