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
    public class ModelsController : ControllerBase
    {
        private readonly ModeBureauDBContext _context;

        public ModelsController(ModeBureauDBContext context)
        {
            _context = context;
        }

        // GET: api/Models
        [HttpGet]
        public IEnumerable<Model> GetModel()
        {
            return _context.Model;
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _context.Model.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Models/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel([FromRoute] long id, [FromBody] Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.modelId)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

        [HttpPost("createModelIncomingTask")]
        public async Task<IActionResult> PostModelIncomingTask([FromBody] ModeltaskDTO @modelIncomingTask)
        {
            ModelIncomingTask modeltask = new ModelIncomingTask();
            modeltask.IncomingTaskId = modelIncomingTask.incomingTaskId;
            modeltask.ModelId = modelIncomingTask.modelId;

            _context.ModelIncomingTask.Add(modeltask);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Created("GetModel", modeltask.ModelId);
        }

        [HttpGet("{id}/all")]
        public async Task<IActionResult> getIncomingTasks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelTasks = _context.ModelIncomingTask.Where(u => u.ModelId == id)
                .Include(d => d.IncomingTask).ToList();

            List<IncomingTask> tasks = new List<IncomingTask>();

            foreach (var task in modelTasks)
            {
                tasks.Add(task.IncomingTask);
            }

            return Ok(tasks);

        }


        // POST: api/Models
        [HttpPost]
        public async Task<IActionResult> PostModel([FromBody] Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Model.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.modelId }, model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = await _context.Model.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Model.Remove(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        private bool ModelExists(long id)
        {
            return _context.Model.Any(e => e.modelId == id);
        }
    }
}