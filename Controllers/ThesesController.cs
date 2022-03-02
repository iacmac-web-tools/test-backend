#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackTask.Data;
using BackTask.Models;

namespace BackTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class thesesController : ControllerBase
    {
        private readonly DataContext _context;

        public thesesController(DataContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Получение пострнаничного списка тезисов
        /// </summary>
        /// GET: api/theses
        [HttpGet]
        // public async Task<ActionResult<IEnumerable<ThesisResource>>> Gettheses()
        public async Task<ActionResult<IEnumerable<ThesisResource>>> Gettheses()
        {
            // return await _context.theses.ToListAsync();

            var theses = await _context.theses.ToListAsync();
            return theses;
        }

        /// <summary>
        /// Получить подробную информацию по одному тезису
        /// </summary>
        // GET: api/theses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThesisResource>> GetThesisResource(int id)
        {
            var thesisResource = await _context.theses.FindAsync(id);

            if (thesisResource == null)
            {
                return NotFound();
            }

            return thesisResource;
        }

        /// <summary>
        /// Изменить тезис
        /// </summary>
        // PUT: api/theses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThesisResource(int id, ThesisResource thesisResource)
        {
            if (id != thesisResource.Id)
            {
                return BadRequest();
            }

            _context.Entry(thesisResource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThesisResourceExists(id))
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

        /// <summary>
        /// Добавить новый тезис
        /// </summary>
        // POST: api/theses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ThesisResource>> PostThesisResource(ThesisResource thesisResource)
        {
            _context.theses.Add(thesisResource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThesisResource", new { id = thesisResource.Id }, thesisResource);
        }

        /// <summary>
        /// Удалить тезис
        /// </summary>
        // DELETE: api/theses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThesisResource(int id)
        {
            var thesisResource = await _context.theses.FindAsync(id);
            if (thesisResource == null)
            {
                return NotFound();
            }

            _context.theses.Remove(thesisResource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThesisResourceExists(int id)
        {
            return _context.theses.Any(e => e.Id == id);
        }
    }
}
