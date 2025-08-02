using System;

namespace WebFormsUI.Models
{
    public class Ferias
    {
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Funcionario Funcionario { get; set; }
        
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

