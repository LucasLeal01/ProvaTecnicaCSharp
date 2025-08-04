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

## Instruções Para Rodar O Sistema Localmente

### Pré-Requisitos

- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** (2019 ou superior) - [Download](https://www.microsoft.com/sql-server/sql-server-downloads)
  - SQL Server Management Studio (SSMS) - [Download](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms)
- **Node.js** (v18.x ou superior) e npm (para Angular) - [Download](https://nodejs.org/)
- **Angular CLI** (v16.0.0) - Instale via npm: `npm install -g @angular/cli@16.0.0`
- **Visual Studio 2022** (para WebForms) - [Download](https://visualstudio.microsoft.com/)
  - Workloads necessárias: ASP.NET e desenvolvimento web
- **Visual Studio Code** (para API e Angular) - [Download](https://code.visualstudio.com/)
  - Extensões recomendadas: C# Dev Kit, Angular Language Service

### Configuração do Ambiente

1. **Clone o repositório**
   ```
   git clone [URL_DO_REPOSITÓRIO]
   cd ProvaTecnicaCSharp
   ```

2. **Restaure as dependências**
   ```
   dotnet restore ProvaTecnicaCSharp.sln
   ```

3. **Configure o banco de dados**
   - Abra o SQL Server Management Studio
   - Conecte-se à sua instância local do SQL Server
   - Execute os scripts na seguinte ordem:
     1. `Database/CreateTables.sql` - Cria o banco de dados e tabelas
     2. `Database/SeedData.sql` - Insere dados iniciais

4. **Configure a string de conexão**
   - Abra o arquivo `Api/appsettings.json`
   - Atualize a string de conexão `DefaultConnection` com suas credenciais do SQL Server:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=SEU_SERVIDOR;Database=ProvaTecnicaDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
     }
     ```
     Substitua `SEU_SERVIDOR` pelo nome da sua instância do SQL Server

### Execução do Sistema

#### Método 1: Script Automatizado

**Windows**:
1. Execute o arquivo `run-projects.bat` com privilégios de administrador

**Linux/Mac**:
1. Dê permissão de execução ao script: `chmod +x run-projects.sh`
2. Execute o script: `./run-projects.sh`

#### Método 2: Execução Manual

##### API (ASP.NET Core)
1. Abra um terminal na pasta raiz do projeto
2. Execute os comandos:
   ```
   cd Api
   dotnet run
   ```
3. A API estará disponível em: http://localhost:5000

##### WebForms (ASP.NET)
1. Abra o arquivo `WebFormsUI/WebFormsUI.sln` no Visual Studio 2022
2. Configure a URL da API no `web.config` se necessário
3. Execute o projeto WebForms usando o IIS Express
4. A interface WebForms estará disponível em: http://localhost:5002 ou https://localhost:44300

##### Angular (Frontend)
1. Abra um terminal na pasta raiz do projeto
2. Execute os comandos:
   ```
   cd AngularUI
   npm install
   ng serve --open
   ```
3. A interface Angular estará disponível em: http://localhost:4200

### Considerações Sobre O Banco De Dados

#### Configuração Manual do Banco de Dados

1. **Criar o banco de dados**
   - O script `Database/CreateTables.sql` cria automaticamente o banco `ProvaTecnicaDB` se não existir
   - Cria as tabelas: `Funcionario`, `Ferias` e `HistoricoAlteracao`

2. **Dados iniciais**
   - O script `Database/SeedData.sql` insere dados de exemplo para testes
   - Inclui 3 funcionários, períodos de férias e histórico de alterações

3. **Migração do Entity Framework (alternativa)**
   - Se preferir usar migrações do EF Core em vez dos scripts SQL:
     ```
     cd Api
     dotnet ef database update
     ```

4. **Backup e Restauração**
   - Recomenda-se fazer backup do banco antes de alterações significativas
   - Use o SQL Server Management Studio para backup/restauração

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
├── Api/                    # Web API REST em ASP.NET Core
│   ├── Controllers/        # Controladores da API
│   ├── Data/               # Contexto e configurações do EF Core
│   ├── Models/             # Modelos de dados
│   ├── Services/           # Serviços da aplicação
│   └── Program.cs          # Ponto de entrada da aplicação
├── WebFormsUI/             # Interface WebForms tradicional
│   ├── Models/             # Modelos de dados
│   ├── Helpers/            # Classes auxiliares
│   └── *.aspx              # Páginas WebForms
├── AngularUI/              # Interface moderna em Angular
│   ├── src/                # Código fonte Angular
│   └── package.json        # Dependências do projeto
├── Core/                   # Camada de negócios compartilhada
├── Database/               # Scripts SQL
│   ├── CreateTables.sql    # Criação do banco e tabelas
│   └── SeedData.sql        # Dados iniciais
└── run-projects.*          # Scripts para execução rápida
```

## Tecnologias Utilizadas

### Backend
- **ASP.NET Core 8.0**
- **Entity Framework Core 8.0** - ORM para acesso a dados
- **SQL Server** - Sistema de banco de dados relacional
- **Web API REST** - Arquitetura para comunicação entre sistemas
- **Swagger/OpenAPI** - Documentação da API

### Frontend
- **Angular 16** - Framework para SPA
- **ASP.NET WebForms** - Framework tradicional para aplicações web
- **Bootstrap 5.1.3** - Framework CSS para design responsivo
- **Font Awesome 6.4.0** - Biblioteca de ícones
- **CSS3 Moderno** - Grid, Flexbox para layouts avançados
- **JavaScript ES6+** - Recursos modernos de JavaScript

---

**© 2024 Sistema de Gerenciamento**

