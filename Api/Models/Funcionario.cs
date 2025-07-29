using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Cargo { get; set; } = string.Empty;
        
        [Required]
        public DateTime DataAdmissao { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salario { get; set; }
        
        // Propriedade de navegação
        public virtual ICollection<Ferias> Ferias { get; set; } = new List<Ferias>();
    }
}

