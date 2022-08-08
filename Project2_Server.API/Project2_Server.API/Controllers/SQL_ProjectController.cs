using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project2_Server.Data;
using Project2_Server.Model;

namespace Project2_Server.API.Controllers
{
    [Route("api/SQL_Project")]
    [ApiController]
    public class SQL_ProjectController : ControllerBase
    {
        private readonly SQL_Project _repo;

        public SQL_ProjectController(SQL_Project repo) => _repo = repo;

        [HttpGet]
        public async Task<IEnumerable<DMODEL_Project>> Get()
            => await _repo.DMODEL_Project.ToListAsync();

        [HttpGet("project_id")]
        [ProducesResponseType(typeof(DMODEL_Project), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int project_id)
        {
            var Project = await _repo.DMODEL_Project.FindAsync(project_id);
            return Project == null ? NotFound() : Ok(Project);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(DMODEL_Project Project)
        {
            await _repo.DMODEL_Project.AddAsync(Project);
            await _repo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = Project.project_id }, Project);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int project_id, DMODEL_Project Project)
        {
            if (project_id != Project.project_id) return BadRequest();

            _repo.Entry(Project).State = EntityState.Modified;
            await _repo.SaveChangesAsync();

            return NoContent();


        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int project_id)
        {
            var ProjectToDelete = await _repo.DMODEL_Project.FindAsync(project_id);
            if (ProjectToDelete == null) return NotFound();

            _repo.DMODEL_Project.Remove(ProjectToDelete);
            await _repo.SaveChangesAsync();

            return NoContent();
        }
    }
}
