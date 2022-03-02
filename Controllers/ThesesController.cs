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
    [Produces("application/json")]
    [ApiController]
    public class thesesController : ControllerBase
    {
        private readonly DataContext _context;

        public thesesController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение полного списка тезисов
        /// </summary>
        /// GET: api/theses/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ThesisTableItemResource>>> GetAlltheses()
        {
            var theses = await _context.theses.ToListAsync();
            List<ThesisTableItemResource> myItems = new List<ThesisTableItemResource>();
            int index = 0;
            foreach(var item in theses){
                myItems.Add(new ThesisTableItemResource());
                myItems[index].Id = item.Id;
                myItems[index].mainAuthor = (item.mainAuthor.lastName + ' ' + item.mainAuthor.firstName + ' ' + item.mainAuthor.middleName);
                myItems[index].contactEmail = item.contactEmail;
                myItems[index].topic = item.topic;
                myItems[index].created = item.created;
                myItems[index].updated = item.updated;
                index++;
            }
            return myItems;
        }

        /// <summary>
        /// Получение постраничного списка тезисов
        /// </summary>
        /// GET: api/theses/all
        [HttpGet]
        public async Task<ActionResult<ThesisTableItemResourceDataTableResult>> Gettheses(int page = 1, int pageSize = 10)
        {
            var theses = await _context.theses.ToListAsync();
            
            ThesisTableItemResourceDataTableResult myItemsTable = new ThesisTableItemResourceDataTableResult();
            myItemsTable.page = page;
            myItemsTable.pageSize = pageSize;
            myItemsTable.totalItems = theses.Count;
            double tP = Convert.ToDouble(myItemsTable.totalItems)/Convert.ToDouble(myItemsTable.pageSize);
            myItemsTable.totalPages = Convert.ToInt32(Math.Ceiling(tP));

            int startIdx = myItemsTable.pageSize * (myItemsTable.page - 1);
            int endIdx = 0;

            if((startIdx + myItemsTable.pageSize) < myItemsTable.totalItems){
                endIdx = startIdx + myItemsTable.pageSize;
            }
            else{
                endIdx = startIdx + (myItemsTable.totalItems - startIdx);
            }

            int idx = 0;

            for(int i = startIdx; i < endIdx; i++){
                myItemsTable.items.Add(new ThesisTableItemResource());
                myItemsTable.items[idx].Id = theses[i].Id;
                myItemsTable.items[idx].mainAuthor = (theses[i].mainAuthor.lastName + ' ' + theses[i].mainAuthor.firstName + ' ' + theses[i].mainAuthor.middleName);
                myItemsTable.items[idx].contactEmail = theses[i].contactEmail;
                myItemsTable.items[idx].topic = theses[i].topic;
                myItemsTable.items[idx].created = theses[i].created;
                myItemsTable.items[idx].updated = theses[i].updated;
                idx++;
            }

            return myItemsTable;
        }

        /// <summary>
        /// Получить подробную информацию по одному тезису
        /// </summary>
        // GET: api/theses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThesisResource>> GetThesisResource(int id = 1)
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
        public async Task<IActionResult> PutThesisResource(int id, ThesisForm thesisForm)
        {
            var thesisResource = await _context.theses.FindAsync(id);
            
            if (thesisResource == null)
            {
                return NotFound();
            }

            thesisResource.mainAuthor = thesisForm.mainAuthor;
            thesisResource.contactEmail = thesisForm.contactEmail;
            thesisResource.otherAuthors = thesisForm.otherAuthors;
            thesisResource.topic = thesisForm.topic;
            thesisResource.content = thesisForm.content;
            thesisResource.updated = DateTime.Now;

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
        public async Task<ActionResult<ThesisResource>> PostThesisResource(ThesisForm thesisForm)
        {
            ThesisResource newTheses = new ThesisResource();
            newTheses.mainAuthor = thesisForm.mainAuthor;
            newTheses.contactEmail = thesisForm.contactEmail;
            newTheses.otherAuthors = thesisForm.otherAuthors;
            newTheses.topic = thesisForm.topic;
            newTheses.content = thesisForm.content;
            newTheses.created = DateTime.Now;
            newTheses.updated = DateTime.Now;

            _context.theses.Add(newTheses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThesisResource", new { id = newTheses.Id }, newTheses);
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
