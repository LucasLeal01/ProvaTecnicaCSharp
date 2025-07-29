import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FuncionarioService } from '../../services/funcionario.service';
import { Funcionario } from '../../models/funcionario.model';

@Component({
  selector: 'app-funcionario-form',
  templateUrl: './funcionario-form.component.html',
  styleUrls: ['./funcionario-form.component.css']
})
export class FuncionarioFormComponent implements OnInit {
  @Input() funcionario: Funcionario | null = null;
  @Output() submitted = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  funcionarioForm: FormGroup;
  loading = false;
  error = '';

  constructor(
    private fb: FormBuilder,
    private funcionarioService: FuncionarioService
  ) {
    this.funcionarioForm = this.fb.group({
      nome: ['', [Validators.required, Validators.maxLength(100)]],
      cargo: ['', [Validators.required, Validators.maxLength(50)]],
      dataAdmissao: ['', Validators.required],
      salario: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    if (this.funcionario) {
      this.funcionarioForm.patchValue({
        nome: this.funcionario.nome,
        cargo: this.funcionario.cargo,
        dataAdmissao: this.funcionario.dataAdmissao.split('T')[0], // Format for date input
        salario: this.funcionario.salario
      });
    }
  }

  onSubmit(): void {
    if (this.funcionarioForm.valid) {
      this.loading = true;
      this.error = '';

      const formData = this.funcionarioForm.value;
      const funcionarioData: Funcionario = {
        id: this.funcionario?.id || 0,
        nome: formData.nome,
        cargo: formData.cargo,
        dataAdmissao: formData.dataAdmissao,
        salario: parseFloat(formData.salario)
      };

      const operation = this.funcionario 
        ? this.funcionarioService.updateFuncionario(this.funcionario.id, funcionarioData)
        : this.funcionarioService.createFuncionario(funcionarioData);

      operation.subscribe({
        next: () => {
          this.loading = false;
          this.submitted.emit();
        },
        error: (error) => {
          this.error = 'Erro ao salvar funcionário: ' + error.message;
          this.loading = false;
        }
      });
    }
  }

  onCancel(): void {
    this.cancelled.emit();
  }

  get isEditing(): boolean {
    return this.funcionario !== null;
  }
}

