using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ferias>>> GetFerias()
        {
            return await _context.Ferias
                .Include(f => f.Funcionario)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ferias>> GetFerias(int id)
        {
            var ferias = await _context.Ferias
                .Include(f => f.Funcionario)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (ferias == null)
            {
                return NotFound();
            }

            return ferias;
        }

        [HttpGet("funcionario/{funcionarioId}")]
        public async Task<ActionResult<IEnumerable<Ferias>>> GetFeriasByFuncionario(int funcionarioId)
        {
            var ferias = await _context.Ferias
                .Include(f => f.Funcionario)
                .Where(f => f.FuncionarioId == funcionarioId)
                .ToListAsync();

            return ferias;
        }

        [HttpPost]
        public async Task<ActionResult<Ferias>> PostFerias(Ferias ferias)
        {
            var funcionarioExists = await _context.Funcionarios.AnyAsync(f => f.Id == ferias.FuncionarioId);
            if (!funcionarioExists)
            {
                return BadRequest("Funcionário não encontrado.");
            }

            if (ferias.DataFim <= ferias.DataInicio)
            {
                return BadRequest("A data de fim deve ser posterior à data de início.");
            }

            _context.Ferias.Add(ferias);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFerias), new { id = ferias.Id }, ferias);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFerias(int id, Ferias ferias)
        {
            if (id != ferias.Id)
            {
                return BadRequest();
            }

            var funcionarioExists = await _context.Funcionarios.AnyAsync(f => f.Id == ferias.FuncionarioId);
            if (!funcionarioExists)
            {
                return BadRequest("Funcionário não encontrado.");
            }

            if (ferias.DataFim <= ferias.DataInicio)
            {
                return BadRequest("A data de fim deve ser posterior à data de início.");
            }

            _context.Entry(ferias).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeriasExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFerias(int id)
        {
            var ferias = await _context.Ferias.FindAsync(id);
            if (ferias == null)
            {
                return NotFound();
            }

            _context.Ferias.Remove(ferias);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeriasExists(int id)
        {
            return _context.Ferias.Any(e => e.Id == id);
        }
    }
}

