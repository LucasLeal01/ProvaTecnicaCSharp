using System;
using System.Collections.Generic;

namespace WebFormsUI.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public decimal Salario { get; set; }
        public List<Ferias> Ferias { get; set; }

        public Funcionario()
        {
            Nome = string.Empty;
            Cargo = string.Empty;
            Ferias = new List<Ferias>();
        }
    }
}

