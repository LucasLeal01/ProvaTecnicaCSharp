import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NotificationService } from '../../services/notification.service';
import { FuncionarioService } from '../../services/funcionario.service';
import { Funcionario } from '../../models/funcionario.model';

@Component({
  selector: 'app-ferias',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './ferias.component.html',
  styleUrls: ['./ferias.component.css']
})
export class FeriasComponent implements OnInit {
  feriasForm: FormGroup;
  ferias: any[] = [];
  funcionarios: Funcionario[] = [];
  loading = false;
  error = '';
  showForm = false;

  constructor(
    private fb: FormBuilder,
    private notificationService: NotificationService,
    private funcionarioService: FuncionarioService
  ) {
    this.feriasForm = this.fb.group({
      funcionarioId: ['', Validators.required],
      dataInicio: ['', Validators.required],
      dataFim: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    // Primeiro carregamos os funcionários, depois as férias
    this.carregarFuncionarios();
  }

  carregarFuncionarios(): void {
    this.loading = true;
    this.funcionarioService.getFuncionarios().subscribe({
      next: (data) => {
        this.funcionarios = data;
        // Após carregar os funcionários, carregamos as férias
        this.carregarFerias();
      },
      error: (error) => {
        this.error = 'Erro ao carregar funcionários';
        this.loading = false;
      }
    });
  }

  carregarFerias(): void {
    this.loading = true;
    this.error = '';
    
    this.funcionarioService.getFerias().subscribe({
      next: (data) => {
        // Adicionar o nome do funcionário para cada registro de férias
        this.ferias = data.map(ferias => {
          const funcionario = this.funcionarios.find(f => f.id === ferias.funcionarioId);
          return {
            ...ferias,
            funcionarioNome: funcionario?.nome || 'Funcionário não encontrado'
          };
        });
        this.loading = false;
      },
      error: (error) => {
        this.error = 'Erro ao carregar férias';
        this.loading = false;
      }
    });
  }

  toggleForm(): void {
    this.showForm = !this.showForm;
    if (this.showForm) {
      this.feriasForm.reset();
    }
  }

  onSubmit(): void {
    if (this.feriasForm.invalid) {
      return;
    }

    this.loading = true;
    const funcionarioSelecionado = this.funcionarios.find(f => f.id === +this.feriasForm.value.funcionarioId);
    
    const novaFerias = {
      funcionarioId: +this.feriasForm.value.funcionarioId,
      dataInicio: this.feriasForm.value.dataInicio,
      dataFim: this.feriasForm.value.dataFim
    };

    this.funcionarioService.createFerias(novaFerias).subscribe({
      next: (response) => {
        // Adicionar o nome do funcionário para exibição na tabela
        const feriasComNome = {
          ...response,
          funcionarioNome: funcionarioSelecionado?.nome
        };
        this.ferias.push(feriasComNome);
        this.loading = false;
        this.showForm = false;
        this.notificationService.showSuccess('Férias registradas com sucesso!');
      },
      error: (error) => {
        this.error = 'Erro ao registrar férias';
        this.loading = false;
        this.notificationService.showError('Erro ao registrar férias');
      }
    });
  }

  excluirFerias(id: number): void {
    this.loading = true;
    
    this.funcionarioService.deleteFerias(id).subscribe({
      next: () => {
        this.ferias = this.ferias.filter(f => f.id !== id);
        this.loading = false;
        this.notificationService.showSuccess('Férias excluídas com sucesso!');
      },
      error: (error) => {
        this.error = 'Erro ao excluir férias';
        this.loading = false;
        this.notificationService.showError('Erro ao excluir férias');
      }
    });
  }

  gerarRelatorio(): void {
    this.notificationService.showInfo('Gerando relatório de férias...');
    this.loading = true;
    
    this.funcionarioService.getRelatorioPdf().subscribe({
      next: (blob) => {
        this.loading = false;
        // Criar URL para o blob e abrir em uma nova janela
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = 'relatorio-ferias.pdf';
        link.click();
        window.URL.revokeObjectURL(url);
        this.notificationService.showSuccess('Relatório de férias gerado com sucesso!');
      },
      error: (error) => {
        this.loading = false;
        this.error = 'Erro ao gerar relatório';
        this.notificationService.showError('Erro ao gerar relatório de férias');
      }
    });
  }
}