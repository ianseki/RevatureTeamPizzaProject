using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_Server.Data;
using Project2_Server.Model;




namespace Project2_Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SQL_CustomerController : ControllerBase
    {
        private readonly SQL_Customer _repo;
        //private readonly SQL_Customer customers;

        /*public SQL_CustomerController(SQL_Customer customer)
        {
            this.customers = customer;
        }*/

        public SQL_CustomerController(SQL_Customer repo) => _repo = repo; //SQL_Customer from Data

        [HttpGet]
        public async Task<IEnumerable<DMODEL_Customer>> Get()
            => await _repo.repo.ToListAsync(); 

        /*public IActionResult GetSQL_Customer()
        {
            return Ok(customers.DBMODEL_Customer.ToList());
        }*/

        [HttpGet("customer_id")]
        [ProducesResponseType(typeof(SQL_Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int customer_id)
        {
            var customers = await _repo.Customers.FindAsync(customer_id);
            return customers == null ? NotFound() : Ok(customers);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(SQL_Customer customers)
        {
            await _repo.Customers.AddAsync(customers);
            //await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = customers.Id }, customers);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int customer_id, SQL_Customer customers)
        {
            if (id != Customers.idreturn BadRequest();

            _customer.Entry(Customers).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var customersToDelete = await _repo.DMODEL_Customer.FindAsync(customer_id);
            if (customersToDelete == null) return NotFound();

            _repo.DMODEL_Customer.Remove(customersToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }



    }
}
