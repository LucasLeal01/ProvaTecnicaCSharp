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
        private readonly ILoggingService _loggingService;

        public FuncionariosController(ApplicationDbContext context, IHistoricoService historicoService, ILoggingService loggingService)
        {
            _context = context;
            _historicoService = historicoService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFuncionarios()
        {
            try
            {
                _loggingService.LogInfo("Iniciando busca de funcionários");
                
                // Verifica se há funcionários no banco
                var count = await _context.Funcionarios.CountAsync();
                _loggingService.LogInfo($"Número de funcionários encontrados: {count}");
                
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

                _loggingService.LogInfo($"Retornando {funcionarios.Count} funcionários");
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Erro ao buscar funcionários: {ex.Message}");
                return StatusCode(500, new { error = "Erro ao buscar funcionários", details = ex.Message });
            }
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
            try
            {
                _context.Funcionarios.Add(funcionario);
                await _context.SaveChangesAsync();

                _loggingService.LogInformation($"Funcionário criado com sucesso: {funcionario.Nome} (ID: {funcionario.Id})");
                return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Erro ao criar funcionário: {funcionario.Nome}", ex);
                return StatusCode(500, new { message = "Erro interno do servidor" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            try
            {
                if (id != funcionario.Id)
                {
                    return BadRequest(new { message = "ID inválido" });
                }

                var funcionarioAntigo = await _context.Funcionarios.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
                
                if (funcionarioAntigo == null)
                {
                    return NotFound(new { message = "Funcionário não encontrado" });
                }

                _context.Entry(funcionario).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                await _historicoService.RegistrarAlteracoes(funcionarioAntigo, funcionario, id);

                _loggingService.LogInformation($"Funcionário atualizado com sucesso: {funcionario.Nome} (ID: {id})");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
                {
                    return NotFound(new { message = "Funcionário não encontrado" });
                }
                else
                {
                    _loggingService.LogError($"Erro de concorrência ao atualizar funcionário ID: {id}");
                    return StatusCode(500, new { message = "Erro interno do servidor" });
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Erro ao atualizar funcionário ID: {id}", ex);
                return StatusCode(500, new { message = "Erro interno do servidor" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FindAsync(id);
                if (funcionario == null)
                {
                    return NotFound(new { message = "Funcionário não encontrado" });
                }

                _context.Funcionarios.Remove(funcionario);
                await _context.SaveChangesAsync();

                _loggingService.LogInformation($"Funcionário excluído com sucesso: {funcionario.Nome} (ID: {id})");
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggingService.LogError($"Erro ao excluir funcionário ID: {id}", ex);
                return StatusCode(500, new { message = "Erro interno do servidor" });
            }
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


