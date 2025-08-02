using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Ferias
    {
        public int Id { get; set; }
        
        [Required]
        public int FuncionarioId { get; set; }
        
        [Required]
        public DateTime DataInicio { get; set; }
        
        [Required]
        public DateTime DataFim { get; set; }
        
        public virtual Funcionario? Funcionario { get; set; }
        
        public string Status
        {
            get
            {
                var hoje = DateTime.Today;
                if (hoje < DataInicio)
                    return "Pendente";
                else if (hoje >= DataInicio && hoje <= DataFim)
                    return "Em andamento";
                else
                    return "Concluída";
            }
        }
    }
}

