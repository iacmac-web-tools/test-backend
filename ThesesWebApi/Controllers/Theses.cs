using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ThesesDomain.Models;

namespace ThesesWebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ThesesController : ControllerBase
    {
        private readonly DataContext _context; public ThesesController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение постраничного списка тезисов
        /// </summary>
        /// <param name="thesisForm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ThesisTableItemResourceDataTableResult>> Gettheses(int page = 1, int pageSize = 10)
        {
            var theses = await _context.theses.ToListAsync();

            ThesisTableItemResourceDataTableResult myItemsTable = new ThesisTableItemResourceDataTableResult();
            myItemsTable.page = page;
            myItemsTable.pageSize = pageSize;
            myItemsTable.totalItems = theses.Count;
            double tP = Convert.ToDouble(myItemsTable.totalItems) / Convert.ToDouble(myItemsTable.pageSize);
            myItemsTable.totalPages = Convert.ToInt32(Math.Ceiling(tP));

            int startIdx = myItemsTable.pageSize * (myItemsTable.page - 1);
            int endIdx = 0;

            if ((startIdx + myItemsTable.pageSize) < myItemsTable.totalItems)
            {
                endIdx = startIdx + myItemsTable.pageSize;
            }
            else
            {
                endIdx = startIdx + (myItemsTable.totalItems - startIdx);
            }

            int idx = 0;

            for (int i = startIdx; i < endIdx; i++)
            {
                myItemsTable.items.Add(new ThesisTableItemResource());
                myItemsTable.items[idx].contactEmail = theses[i].contactEmail;
                myItemsTable.items[idx].topic = theses[i].topic;
                idx++;
            }

            return myItemsTable;

        }

        /// <summary>
        /// Добавить новый тезис
        /// </summary>
        /// <param name="thesisForm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ThesisResource>> PostThesis(ThesisForm thesisForm)
        {
            ThesisResource newThesis = new ThesisResource();
            newThesis.id = thesisForm.Id;
            newThesis.mainAuthor = thesisForm.mainAuthor;
            newThesis.contactEmail = thesisForm.contactEmail;
            newThesis.topic = thesisForm.topic;
            newThesis.content = thesisForm.content;
            newThesis.created = DateTime.UtcNow;
            newThesis.updated = DateTime.UtcNow;

            _context.theses.Add(newThesis);
            await _context.SaveChangesAsync();

            return Ok(newThesis);
        }

        /// <summary>
        /// Получение полного списка тезисов
        /// </summary>
        /// <param name="thesisForm"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ThesisTableItemResource>>> GetAlltheses()
        {
            var theses = await _context.theses.ToListAsync();
            List<ThesisTableItemResource> myItems = new List<ThesisTableItemResource>();
            return myItems;
        }

        /// <summary>
        /// Получить полную информацию по одному тезису
        /// </summary>
        /// <param name="thesisForm"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ThesisResource>> GetThesisResource(int id = 5)
        {
            var thesesResource = await _context.theses.FindAsync(id);
            if (thesesResource == null)
            {
                NotFound();

            }
            return thesesResource;
        }

        /// <summary>
        /// Изменить тезис
        /// </summary>
        /// <param name="thesisForm"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ThesisResource>> PutThesisResource(int id, ThesisForm thesisForm)
        {
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Удалить тезис
        /// </summary>
        /// <param name="thesisForm"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ThesisResource>> DeleteThesis(int id)
        {

            var theses = await _context.theses.FindAsync(id);
            if (theses == null)
            {
                return NotFound();
            }
            _context.theses.Remove(theses);
            await _context.SaveChangesAsync();


            return NoContent();
        }

    }
}