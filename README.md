# Sistema de Gerenciamento - Prova Técnica C#

## Descrição

Sistema de gerenciamento de funcionários e férias desenvolvido em C# com múltiplas interfaces de usuário.

## Arquitetura

O projeto está estruturado em múltiplas camadas:

- **API**: Web API REST em ASP.NET Core
- **WebForms**: Interface WebForms tradicional
- **Angular**: Interface moderna em Angular
- **Core**: Camada de negócios compartilhada
- **Database**: Scripts de banco de dados

## Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- SQL Server
- Node.js (para Angular)
- Visual Studio 2022 (para WebForms)
- Visual Studio Code (para API e Angular)

### Execução

#### API e Angular (Visual Studio Code)
1. Abra a pasta do projeto no Visual Studio Code
2. Configure a string de conexão no `appsettings.json` da API
3. Execute a API: 
   ```
   cd Api
   dotnet run
   ```
4. Execute o Angular: 
   ```
   cd AngularUI
   npm install
   ng serve
   ```

#### WebForms (Visual Studio)
1. Abra o arquivo `WebFormsUI.sln` no Visual Studio 2022
2. Configure a URL da API no `web.config`
3. Execute o projeto WebForms usando o IIS Express

## Funcionalidades

### Funcionários
- Cadastro completo
- Edição de dados
- Exclusão de registros
- Listagem com paginação

### Férias
- Cadastro de períodos
- Associação com funcionários
- Validações de datas
- Histórico de alterações

## Estrutura de Arquivos

```
ProvaTecnicaCSharp/
├── Api/                    # Web API REST
├── WebFormsUI/             # Interface WebForms
├── AngularUI/              # Interface Angular
├── Core/                   # Camada de negócios
└── Database/               # Scripts SQL
```

## Tecnologias Utilizadas

### Backend
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- Web API REST

### Frontend
- Bootstrap 5.1.3
- Font Awesome 6.4.0
- CSS3 Moderno (Grid, Flexbox)
- JavaScript ES6+
- Angular 16

---

**© 2024 Sistema de Gerenciamento**

