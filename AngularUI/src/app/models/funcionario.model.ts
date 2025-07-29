export interface Funcionario {
  id: number;
  nome: string;
  cargo: string;
  dataAdmissao: string;
  salario: number;
  ferias?: Ferias[];
}

export interface Ferias {
  id: number;
  funcionarioId: number;
  dataInicio: string;
  dataFim: string;
  funcionario?: Funcionario;
  status?: string;
}

