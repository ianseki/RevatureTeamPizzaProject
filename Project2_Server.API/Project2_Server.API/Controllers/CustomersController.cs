using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project2_Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Customers _repo;

        public CustomersController(Customers customers) => _repo = customers;

        [HttpGet]
        public async Task<IEnumerable<Customers>> Get()
            => await _repo.Customers.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Customers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var Customers = await _repo.Customers.FindAsync(id);
            return Customers == null ? NotFound() : Ok(Customers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Customers Customers)
        {
            await _repo.Customers.AddAsync(Customers);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Customers.Id }, Customers);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Customers Customers)
        {
            if (id != Customers.Id) return BadRequest();

            _repo.Entry(Customers).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var CustomersToDelete = await _repo.Customers.FindAsync(id);
            if (CustomersToDelete == null) return NotFound();

            _repo.Customers.Remove(CustomersToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }



    }
}
