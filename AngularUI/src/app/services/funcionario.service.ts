import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Funcionario, Ferias } from '../models/funcionario.model';

@Injectable({
  providedIn: 'root'
})
export class FuncionarioService {
  private apiUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  // Funcionários
  getFuncionarios(): Observable<Funcionario[]> {
    return this.http.get<Funcionario[]>(`${this.apiUrl}/funcionarios`);
  }

  getFuncionario(id: number): Observable<Funcionario> {
    return this.http.get<Funcionario>(`${this.apiUrl}/funcionarios/${id}`);
  }

  createFuncionario(funcionario: Funcionario): Observable<Funcionario> {
    return this.http.post<Funcionario>(`${this.apiUrl}/funcionarios`, funcionario);
  }

  updateFuncionario(id: number, funcionario: Funcionario): Observable<any> {
    return this.http.put(`${this.apiUrl}/funcionarios/${id}`, funcionario);
  }

  deleteFuncionario(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/funcionarios/${id}`);
  }

  getSalarioMedio(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/funcionarios/salario-medio`);
  }

  // Férias
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

  // Relatório PDF
  getRelatorioPdf(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/funcionarios/relatorio/pdf`, { 
      responseType: 'blob' 
    });
  }
}

