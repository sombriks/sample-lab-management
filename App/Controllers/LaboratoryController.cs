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
    public class LaboratoryController : ControllerBase
    {
        private readonly ModelsContext _context;

        public LaboratoryController(ModelsContext context)
        {
            _context = context;
        }

        // GET: api/Laboratory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Laboratory>>> GetLaboratories()
        {
            return await _context.Laboratories.ToListAsync();
        }

        // GET: api/Laboratory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Laboratory>> GetLaboratory(long id)
        {
            var laboratory = await _context.Laboratories.FindAsync(id);

            if (laboratory == null)
            {
                return NotFound();
            }

            return laboratory;
        }

        // PUT: api/Laboratory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaboratory(long id, Laboratory laboratory)
        {
            if (id != laboratory.Id)
            {
                return BadRequest();
            }

            _context.Entry(laboratory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LaboratoryExists(id))
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

        // POST: api/Laboratory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laboratory>> PostLaboratory(Laboratory laboratory)
        {
            _context.Laboratories.Add(laboratory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLaboratory", new { id = laboratory.Id }, laboratory);
        }

        // DELETE: api/Laboratory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaboratory(long id)
        {
            var laboratory = await _context.Laboratories.FindAsync(id);
            if (laboratory == null)
            {
                return NotFound();
            }

            _context.Laboratories.Remove(laboratory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LaboratoryExists(long id)
        {
            return _context.Laboratories.Any(e => e.Id == id);
        }
    }
}
