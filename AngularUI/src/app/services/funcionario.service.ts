import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Funcionario, Ferias } from '../models/funcionario.model';
import { NotificationService } from './notification.service';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FuncionarioService {
  private apiUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) { }


  getFuncionarios(): Observable<Funcionario[]> {
    return this.http.get<Funcionario[]>(`${this.apiUrl}/funcionarios`).pipe(
      catchError(error => {
        this.notificationService.showError('Erro ao carregar funcionários');
        return throwError(() => error);
      })
    );
  }

  getFuncionario(id: number): Observable<Funcionario> {
    return this.http.get<Funcionario>(`${this.apiUrl}/funcionarios/${id}`);
  }

  createFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    console.log('Enviando funcionário para API:', funcionario);
    console.log('URL da API:', `${this.apiUrl}/funcionarios`);
    
    return this.http.post<Funcionario>(`${this.apiUrl}/funcionarios`, funcionario).pipe(
      catchError(error => {
        console.error('Erro ao criar funcionário:', error);
        let errorMessage = 'Erro ao criar funcionário';
        
        if (error.error && error.error.message) {
          errorMessage += `: ${error.error.message}`;
        } else if (error.message) {
          errorMessage += `: ${error.message}`;
        } else if (error.status) {
          errorMessage += `: Erro ${error.status}`;
        }
        
        this.notificationService.showError(errorMessage);
        return throwError(() => error);
      })
    );
  }

  updateFuncionario(id: number, funcionario: Funcionario): Observable<any> {
    return this.http.put(`${this.apiUrl}/funcionarios/${id}`, funcionario).pipe(
      catchError(error => {
        this.notificationService.showError('Erro ao atualizar funcionário');
        return throwError(() => error);
      })
    );
  }

  deleteFuncionario(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/funcionarios/${id}`).pipe(
      catchError(error => {
        this.notificationService.showError('Erro ao excluir funcionário');
        return throwError(() => error);
      })
    );
  }

  getSalarioMedio(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/funcionarios/salario-medio`);
  }


  getFerias(): Observable<Ferias[]> {
    return this.http.get<Ferias[]>(`${this.apiUrl}/ferias`);
  }

  getFeriasByFuncionario(funcionarioId: number): Observable<Ferias[]> {
    return this.http.get<Ferias[]>(`${this.apiUrl}/ferias/funcionario/${funcionarioId}`);
  }

  createFerias(ferias: Ferias): Observable<Ferias> {
    return this.http.post<Ferias>(`${this.apiUrl}/ferias`, ferias);
  }

  deleteFerias(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/ferias/${id}`);
  }


  getRelatorioPdf(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/funcionarios/relatorio/pdf`, { 
      responseType: 'blob' 
    });
  }
}

