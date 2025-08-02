using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Models;
using Api.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHistoricoService _historicoService;

        public FuncionariosController(ApplicationDbContext context, IHistoricoService historicoService)
        {
            _context = context;
            _historicoService = historicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFuncionarios()
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Ferias)
                .Select(f => new
                {
                    f.Id,
                    f.Nome,
                    f.Cargo,
                    f.DataAdmissao,
                    f.Salario,
                    Ferias = f.Ferias.Select(fe => new
                    {
                        fe.Id,
                        fe.DataInicio,
                        fe.DataFim
                    })
                })
                .ToListAsync();

            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios
                .Include(f => f.Ferias)
                .Select(f => new
                {
                    f.Id,
                    f.Nome,
                    f.Cargo,
                    f.DataAdmissao,
                    f.Salario,
                    Ferias = f.Ferias.Select(fe => new
                    {
                        fe.Id,
                        fe.DataInicio,
                        fe.DataFim
                    })
                })
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        [HttpGet("salario-medio")]
        public async Task<ActionResult<decimal>> GetSalarioMedio()
        {
            var funcionarios = await _context.Funcionarios.ToListAsync();
            
            if (!funcionarios.Any())
            {
                return Ok(0);
            }

            var salarioMedio = funcionarios.Average(f => f.Salario);
            return Ok(Math.Round(salarioMedio, 2));
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return BadRequest();
            }

            var funcionarioAntigo = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
            
            if (funcionarioAntigo == null)
            {
                return NotFound();
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                
                await _historicoService.RegistrarAlteracoes(funcionarioAntigo, funcionario, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("relatorio/pdf")]
        public async Task<IActionResult> GetRelatorioPdf()
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.Ferias)
                .ToListAsync();

            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Relatório de Funcionários")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            foreach (var funcionario in funcionarios)
                            {
                                var statusFerias = ObterStatusFerias(funcionario.Ferias);
                                
                                x.Item().BorderBottom(1).Padding(10).Column(col =>
                                {
                                    col.Item().Text($"Nome: {funcionario.Nome}").SemiBold();
                                    col.Item().Text($"Cargo: {funcionario.Cargo}");
                                    col.Item().Text($"Data de Admissão: {funcionario.DataAdmissao:dd/MM/yyyy}");
                                    col.Item().Text($"Salário: R$ {funcionario.Salario:N2}");
                                    col.Item().Text($"Status de Férias: {statusFerias}");
                                });
                                
                                x.Item().PaddingVertical(5);
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                        });
                });
            });

            var pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", "relatorio-funcionarios.pdf");
        }

        private string ObterStatusFerias(ICollection<Ferias> ferias)
        {
            if (!ferias.Any())
                return "Pendente";

            var hoje = DateTime.Today;
            var feriasAtual = ferias.OrderByDescending(f => f.DataInicio).FirstOrDefault();

            if (feriasAtual == null)
                return "Pendente";

            if (hoje < feriasAtual.DataInicio)
                return "Pendente";
            else if (hoje >= feriasAtual.DataInicio && hoje <= feriasAtual.DataFim)
                return "Em andamento";
            else
                return "Concluída";
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}

