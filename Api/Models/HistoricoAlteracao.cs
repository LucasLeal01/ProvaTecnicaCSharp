using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class HistoricoAlteracao
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Entidade { get; set; } = string.Empty;
        
        [Required]
        public int EntidadeId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Campo { get; set; } = string.Empty;
        
        public string? ValorAntigo { get; set; }
        
        public string? ValorNovo { get; set; }
        
        [Required]
        public DateTime DataHora { get; set; } = DateTime.Now;
    }
}

