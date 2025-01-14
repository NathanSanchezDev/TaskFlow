using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.Models;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET /projects
        [HttpGet]
        public async Task<IEnumerable<Project>> Get()
        {
            return await _context.Projects.Include(p => p.Tasks).ToListAsync();
        }

        // GET /projects/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> Get(int id)
        {
            var project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
            if (project == null) return NotFound();
            return project;
        }

        // POST /projects
        [HttpPost]
        public async Task<ActionResult<Project>> Post(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
        }

        // PUT /projects/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Project updatedProject)
        {
            if (id != updatedProject.Id) return BadRequest();

            _context.Entry(updatedProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Projects.AnyAsync(p => p.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE /projects/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}