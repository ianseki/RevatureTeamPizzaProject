using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_Server.Data;
using Project2_Server.Model;

namespace Project2_Server.API.Controllers
{
    [ApiController]
    [Route("api/SQL_Employee")]
    public class SQL_EmployeeController : ControllerBase
    {
        private readonly SQL_Employee _repo;

        public SQL_EmployeeController(SQL_Employee repo) => _repo = repo;

        [HttpGet]
        public async Task<IEnumerable<DMODEL_Employee>> Get()
            => await _repo.DMODEL_Employee.ToListAsync();

        [HttpGet("employee_id")]
        [ProducesResponseType(typeof(DMODEL_Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int employee_id)
        {
            var Employee = await _repo.DMODEL_Employee.FindAsync(employee_id);
            return Employee == null ? NotFound() : Ok(Employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(DMODEL_Employee Employee)
        {
            await _repo.DMODEL_Employee.AddAsync(Employee);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Employee.employee_id }, Employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int employee_id, DMODEL_Employee Employee)
        {
            if (employee_id != Employee.employee_id) return BadRequest();

            _repo.Entry(Employee).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int employee_id)
        {
            var EmployeeToDelete = await _repo.DMODEL_Employee.FindAsync(employee_id);
            if (EmployeeToDelete == null) return NotFound();

            _repo.DMODEL_Employee.Remove(EmployeeToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }





    }
}
