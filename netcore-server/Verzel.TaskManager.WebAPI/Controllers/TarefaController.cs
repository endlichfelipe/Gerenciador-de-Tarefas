using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Verzel.TaskManager.WebAPI.Database;
using Verzel.TaskManager.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Verzel.TaskManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ApiContext _context;

        public TarefaController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/<TarefaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> Get()
        {
            return await _context.Tarefas.ToListAsync();
        }

        // GET api/<TarefaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> Get(long id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        // POST api/<TarefaController>
        [HttpPost]
        public async Task<ActionResult<Tarefa>> Post([FromBody] Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = tarefa.Id }, tarefa);
        }

        // PUT api/<TarefaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TarefaExistsAsync(id))
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

        // DELETE api/<TarefaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> TarefaExistsAsync(long id) => 
            await _context.Tarefas.AnyAsync(t => t.Id == id);
    }
}
