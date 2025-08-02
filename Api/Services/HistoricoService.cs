using Api.Data;
using Api.Models;
using System.Reflection;

namespace Api.Services
{
    public interface IHistoricoService
    {
        Task RegistrarAlteracoes<T>(T entidadeAntiga, T entidadeNova, int entidadeId) where T : class;
    }

    public class HistoricoService : IHistoricoService
    {
        private readonly ApplicationDbContext _context;

        public HistoricoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarAlteracoes<T>(T entidadeAntiga, T entidadeNova, int entidadeId) where T : class
        {
            var nomeEntidade = typeof(T).Name;
            var propriedades = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propriedade in propriedades)
            {
                if (propriedade.Name == "Id" || 
                    propriedade.PropertyType.IsClass && propriedade.PropertyType != typeof(string) ||
                    propriedade.PropertyType.IsGenericType)
                    continue;

                var valorAntigo = propriedade.GetValue(entidadeAntiga)?.ToString();
                var valorNovo = propriedade.GetValue(entidadeNova)?.ToString();

                if (valorAntigo != valorNovo)
                {
                    var historico = new HistoricoAlteracao
                    {
                        Entidade = nomeEntidade,
                        EntidadeId = entidadeId,
                        Campo = propriedade.Name,
                        ValorAntigo = valorAntigo,
                        ValorNovo = valorNovo,
                        DataHora = DateTime.Now
                    };

                    _context.HistoricoAlteracoes.Add(historico);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}

