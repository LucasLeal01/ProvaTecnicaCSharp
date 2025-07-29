using System;
using System.Collections.Generic;

namespace WebFormsUI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public DateTime DataAdmissao { get; set; }
        public decimal Salario { get; set; }
        public List<Ferias> Ferias { get; set; } = new List<Ferias>();
    }
}

