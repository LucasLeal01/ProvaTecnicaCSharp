import { Component, OnInit } from '@angular/core';
import { FuncionarioService } from '../../services/funcionario.service';
import { Funcionario } from '../../models/funcionario.model';

@Component({
  selector: 'app-funcionarios-list',
  templateUrl: './funcionarios-list.component.html',
  styleUrls: ['./funcionarios-list.component.css']
})
export class FuncionariosListComponent implements OnInit {
  funcionarios: Funcionario[] = [];
  salarioMedio: number = 0;
  loading = false;
  error = '';
  showForm = false;
  editingFuncionario: Funcionario | null = null;

  constructor(private funcionarioService: FuncionarioService) { }

  ngOnInit(): void {
    this.loadFuncionarios();
    this.loadSalarioMedio();
  }

  loadFuncionarios(): void {
    this.loading = true;
    this.funcionarioService.getFuncionarios().subscribe({
      next: (data) => {
        this.funcionarios = data;
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Erro ao carregar funcionários: ' + error.message;
        this.loading = false;
      }
    });
  }

  loadSalarioMedio(): void {
    this.funcionarioService.getSalarioMedio().subscribe({
      next: (data) => {
        this.salarioMedio = data;
      },
      error: (error) => {
        console.error('Erro ao carregar salário médio:', error);
      }
    });
  }

  showNewForm(): void {
    this.editingFuncionario = null;
    this.showForm = true;
  }

  editFuncionario(funcionario: Funcionario): void {
    this.editingFuncionario = { ...funcionario };
    this.showForm = true;
  }

  deleteFuncionario(id: number): void {
    if (confirm('Deseja realmente excluir este funcionário?')) {
      this.funcionarioService.deleteFuncionario(id).subscribe({
        next: () => {
          this.loadFuncionarios();
          this.loadSalarioMedio();
        },
        error: (error) => {
          this.error = 'Erro ao excluir funcionário: ' + error.message;
        }
      });
    }
  }

  onFormSubmitted(): void {
    this.showForm = false;
    this.editingFuncionario = null;
    this.loadFuncionarios();
    this.loadSalarioMedio();
  }

  onFormCancelled(): void {
    this.showForm = false;
    this.editingFuncionario = null;
  }

  downloadRelatorio(): void {
    this.funcionarioService.getRelatorioPdf().subscribe({
      next: (blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = 'relatorio-funcionarios.pdf';
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
      },
      error: (error) => {
        this.error = 'Erro ao baixar relatório: ' + error.message;
      }
    });
  }
}

