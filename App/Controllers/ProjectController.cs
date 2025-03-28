using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sample_lab_management.App.Models;

namespace sample_lab_management.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ModelsContext _context;

        public ProjectController(ModelsContext context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects(string q="")
        {
            return await _context.Projects
                .Where(p => p.Name != null && p.Name.Contains(q))
                .ToListAsync();
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            var project = await _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.Students)
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Project/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(long id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Project
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(long id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/student/{studentId}")]
        public async Task<IActionResult> AddStudent(long id, long studentId)
        {
            var project = await _context.Projects.FindAsync(id);
            var student = await _context.Students.FindAsync(studentId);
            
            if (project == null || student == null)
            {
                return NotFound();
            }
            
            project.Students.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}/student/{studentId}")]
        public async Task<IActionResult> RemoveStudent(long id, long studentId)
        {
            var project = await _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.Students)
                .FirstOrDefaultAsync();
            var student = await _context.Students.FindAsync(studentId);
            
            if (project == null || student == null)
            {
                return NotFound();
            }
            
            project.Students.Remove(student);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        private bool ProjectExists(long id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
