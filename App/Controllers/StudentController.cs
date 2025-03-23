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
    public class StudentController : ControllerBase
    {
        private readonly ModelsContext _context;

        public StudentController(ModelsContext context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents(string q = "")
        {
            return await _context.Students
                .Where(s => s.Name != null && s.Name.Contains(q))
                .ToListAsync();
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(long id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Laboratories)
                .Include(s => s.Projects)
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Student/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(long id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Student
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/project/{projectId}")]
        public async Task<IActionResult> AddProject(long id, long projectId)
        {
            var student = await _context.Students.FindAsync(id);
            var project = await _context.Projects.FindAsync(projectId);
            
            if (student == null || project == null)
            {
                return NotFound();
            }

            student.Projects.Add(project);
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
        [HttpDelete("{id}/project/{projectId}")]
        public async Task<IActionResult> DeleteProject(long id, long projectId)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Projects)
                .FirstOrDefaultAsync();
            var project = await _context.Projects.FindAsync(projectId);
            
            if (student == null || project == null)
            {
                return NotFound();
            }
            
            student.Projects.Remove(project);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPut("{id}/laboratory/{laboratoryId}")]
        public async Task<IActionResult> AddLaboratory(long id, long laboratoryId)
        {
            var student = await _context.Students.FindAsync(id);
            var laboratory = await _context.Laboratories.FindAsync(laboratoryId);
            
            if (student == null || laboratory == null)
            {
                return NotFound();
            }

            student.Laboratories.Add(laboratory);
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
        [HttpDelete("{id}/laboratory/{laboratoryId}")]
        public async Task<IActionResult> DeleteLaboratory(long id, long laboratoryId)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Laboratories)
                .FirstOrDefaultAsync();
            var laboratory = await _context.Laboratories.FindAsync(laboratoryId);
            
            if (student == null || laboratory == null)
            {
                return NotFound();
            }
            
            student.Laboratories.Remove(laboratory);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
        
        private bool StudentExists(long id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
