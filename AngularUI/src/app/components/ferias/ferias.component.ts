import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-ferias',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ferias.component.html',
  styleUrls: ['./ferias.component.css']
})
export class FeriasComponent implements OnInit {
  feriasForm: FormGroup;
  ferias: any[] = [];
  loading = false;
  error = '';
  showForm = false;

  constructor(
    private fb: FormBuilder,
    private notificationService: NotificationService
  ) {
    this.feriasForm = this.fb.group({
      funcionarioId: ['', Validators.required],
      funcionarioNome: ['', Validators.required],
      dataInicio: ['', Validators.required],
      dataFim: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.carregarFerias();
  }

  carregarFerias(): void {
    this.loading = true;
    this.error = '';
    
    setTimeout(() => {
      this.ferias = [
        { id: 1, funcionarioNome: 'João Silva', dataInicio: '2023-07-01', dataFim: '2023-07-15' },
        { id: 2, funcionarioNome: 'Maria Oliveira', dataInicio: '2023-08-10', dataFim: '2023-08-25' },
        { id: 3, funcionarioNome: 'Pedro Santos', dataInicio: '2023-09-05', dataFim: '2023-09-20' }
      ];
      this.loading = false;
    }, 1000);
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
    const novaFerias = {
      id: this.ferias.length + 1,
      funcionarioNome: this.feriasForm.value.funcionarioNome,
      dataInicio: this.feriasForm.value.dataInicio,
      dataFim: this.feriasForm.value.dataFim
    };

    setTimeout(() => {
      this.ferias.push(novaFerias);
      this.loading = false;
      this.showForm = false;
      this.notificationService.showSuccess('Férias registradas com sucesso!');
    }, 1000);
  }

  excluirFerias(id: number): void {
    this.loading = true;
    
    setTimeout(() => {
      this.ferias = this.ferias.filter(f => f.id !== id);
      this.loading = false;
      this.notificationService.showSuccess('Férias excluídas com sucesso!');
    }, 1000);
  }

  gerarRelatorio(): void {
    this.notificationService.showInfo('Gerando relatório de férias...');
    
    setTimeout(() => {
      this.notificationService.showSuccess('Relatório de férias gerado com sucesso!');
    }, 2000);
  }
}