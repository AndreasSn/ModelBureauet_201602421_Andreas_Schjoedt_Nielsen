using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModeBureauDB.Models;

namespace ModeBureauDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingTasksController : ControllerBase
    {
        private readonly ModeBureauDBContext _context;

        public IncomingTasksController(ModeBureauDBContext context)
        {
            _context = context;
        }

        // GET: api/IncomingTasks
        [HttpGet]
        public IEnumerable<IncomingTask> GetIncomingTask()
        {
            return _context.IncomingTask;
        }

        // GET: api/IncomingTasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomingTask([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incomingTask = await _context.IncomingTask.FindAsync(id);

            if (incomingTask == null)
            {
                return NotFound();
            }

            return Ok(incomingTask);
        }

        // PUT: api/IncomingTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncomingTask([FromRoute] long id, [FromBody] IncomingTask incomingTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != incomingTask.IncomingTaskId)
            {
                return BadRequest();
            }

            _context.Entry(incomingTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomingTaskExists(id))
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

        // POST: api/IncomingTasks
        [HttpPost]
        public async Task<IActionResult> PostIncomingTask([FromBody] IncomingTask incomingTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.IncomingTask.Add(incomingTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncomingTask", new { id = incomingTask.IncomingTaskId }, incomingTask);
        }

        // DELETE: api/IncomingTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomingTask([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var incomingTask = await _context.IncomingTask.FindAsync(id);
            if (incomingTask == null)
            {
                return NotFound();
            }

            _context.IncomingTask.Remove(incomingTask);
            await _context.SaveChangesAsync();

            return Ok(incomingTask);
        }

        private bool IncomingTaskExists(long id)
        {
            return _context.IncomingTask.Any(e => e.IncomingTaskId == id);
        }
    }
}