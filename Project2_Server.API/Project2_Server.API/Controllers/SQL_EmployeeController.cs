using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2_Server.Data;
using Project2_Server.Model;

namespace Project2_Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SQL_EmployeeController : ControllerBase
    {
        private readonly SQL_Employee _repo;

        public SQL_EmployeeController(SQL_Employee repo) => _repo = repo;

        [HttpGet]
        public async Task<IEnumerable<DMODEL_Employee>> Get()
            => await _repo.repo.ToListAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Employees), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var Employees = await _repo.Employees.FindAsync(id);
            return Employees == null ? NotFound() : Ok(Employees);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Employees Employees)
        {
            await _repo.Employees.AddAsync(Employees);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Employees.Id }, Employees);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Employees Employees)
        {
            if (id != Employees.Id) return BadRequest();

            _repo.Entry(Employees).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var EmployeesToDelete = await _repo.Employees.FindAsync(id);
            if (EmployeesToDelete == null) return NotFound();

            _repo.Employees.Remove(EmployeesToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }





    }
}
