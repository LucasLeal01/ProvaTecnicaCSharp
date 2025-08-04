import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentYear: number = new Date().getFullYear();
  title = 'Sistema de Gerenciamento';
  subtitle = 'Gerencie funcionarios e ferias de forma eficiente';
  
  ngOnInit(): void {
    // Inicialização do componente
  }
  
  cards = [
    {
      title: 'Funcionarios',
      icon: 'fas fa-users',
      description: 'Gerencie o cadastro completo de funcionarios com recursos avancados de criacao, edicao e exclusao de registros. Interface intuitiva e responsiva.',
      link: '/funcionarios',
      buttonText: 'Acessar',
      type: 'primary'
    },
    {
      title: 'Ferias',
      icon: 'fas fa-umbrella-beach',
      description: 'Controle total sobre as ferias dos funcionarios. Cadastre periodos, visualize historico e gerencie solicitacoes de forma organizada.',
      link: '/ferias',
      buttonText: 'Acessar',
      type: 'success'
    }
  ];
}