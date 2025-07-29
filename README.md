# Prova Técnica Prática – Desenvolvedor C# Júnior

Este projeto é uma solução completa para gerenciamento de funcionários e férias, desenvolvida como parte de uma prova técnica para desenvolvedor C# júnior.

## Estrutura do Projeto

A solução contém três projetos principais:

- **Api**: ASP.NET Core Web API (.NET 8) - Backend com CRUD e geração de PDF
- **WebFormsUI**: ASP.NET WebForms (.NET 8) - Interface web tradicional
- **AngularUI**: Angular 16 - Interface web moderna (opcional)

## Tecnologias Utilizadas

### Backend (Api)
- ASP.NET Core 8.0
- Entity Framework Core 8.0
- SQL Server LocalDB
- QuestPDF 2023.12.6 para geração de relatórios
- Swagger para documentação da API

### Frontend WebForms (WebFormsUI)
- ASP.NET WebForms (.NET 8)
- Bootstrap 5.1.3
- jQuery 3.6.0
- HttpClient para consumo da API

### Frontend Angular (AngularUI)
- Angular 16
- TypeScript 5.0
- Bootstrap 5.1.3
- Reactive Forms
- HttpClient

## Banco de Dados

O projeto utiliza SQL Server LocalDB com três tabelas principais:

### Funcionario
- Id (int, PK, Identity)
- Nome (nvarchar(100), NOT NULL)
- Cargo (nvarchar(50), NOT NULL)
- DataAdmissao (date, NOT NULL)
- Salario (decimal(10,2), NOT NULL)

### Ferias
- Id (int, PK, Identity)
- FuncionarioId (int, FK, NOT NULL)
- DataInicio (date, NOT NULL)
- DataFim (date, NOT NULL)

### HistoricoAlteracao
- Id (int, PK, Identity)
- Entidade (nvarchar(50), NOT NULL)
- EntidadeId (int, NOT NULL)
- Campo (nvarchar(50), NOT NULL)
- ValorAntigo (nvarchar(MAX))
- ValorNovo (nvarchar(MAX))
- DataHora (datetime2, NOT NULL, DEFAULT GETDATE())

## Funcionalidades

### API Endpoints

#### Funcionários
- `GET /api/funcionarios` - Lista todos os funcionários
- `GET /api/funcionarios/{id}` - Busca funcionário por ID
- `POST /api/funcionarios` - Cria novo funcionário
- `PUT /api/funcionarios/{id}` - Atualiza funcionário (com histórico de alterações)
- `DELETE /api/funcionarios/{id}` - Remove funcionário
- `GET /api/funcionarios/salario-medio` - Retorna salário médio
- `GET /api/funcionarios/relatorio/pdf` - Gera relatório em PDF

#### Férias
- `GET /api/ferias` - Lista todas as férias
- `GET /api/ferias/{id}` - Busca férias por ID
- `GET /api/ferias/funcionario/{funcionarioId}` - Lista férias por funcionário
- `POST /api/ferias` - Cria nova férias
- `PUT /api/ferias/{id}` - Atualiza férias
- `DELETE /api/ferias/{id}` - Remove férias

### WebForms UI
- **Funcionarios.aspx**: CRUD completo de funcionários com GridView e DetailsView
- **Ferias.aspx**: Gerenciamento de férias com DropDownList e GridView
- Geração e download de relatórios PDF
- Integração com API via HttpClient

### Angular UI
- **funcionarios-list**: Lista de funcionários com ações de CRUD
- **funcionario-form**: Formulário reativo para criar/editar funcionários
- Exibição de salário médio
- Download de relatórios PDF
- Integração com API via HttpClient

## Pré-requisitos

- .NET 8.0 SDK
- SQL Server LocalDB
- Node.js 18+ (para o projeto Angular)
- Visual Studio 2022 ou Visual Studio Code

## Configuração e Execução

### 1. Configurar o Banco de Dados

Execute os scripts SQL na seguinte ordem:

```bash
# Navegar para o diretório Database
cd Database

# Executar script de criação das tabelas
sqlcmd -S "(localdb)\mssqllocaldb" -i CreateTables.sql

# Executar script de dados de exemplo
sqlcmd -S "(localdb)\mssqllocaldb" -i SeedData.sql
```

### 2. Executar a API

```bash
# Navegar para o diretório da API
cd Api

# Restaurar pacotes NuGet
dotnet restore

# Executar a aplicação
dotnet run
```

A API estará disponível em:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: http://localhost:5000/swagger

### 3. Executar o WebForms UI

```bash
# Navegar para o diretório WebForms
cd WebFormsUI

# Restaurar pacotes NuGet
dotnet restore

# Executar a aplicação
dotnet run
```

O WebForms estará disponível em:
- HTTP: http://localhost:5002
- HTTPS: https://localhost:5003

### 4. Executar o Angular UI (Opcional)

```bash
# Navegar para o diretório Angular
cd AngularUI

# Instalar dependências
npm install

# Executar a aplicação
npm start
```

O Angular estará disponível em:
- HTTP: http://localhost:4200

## Configurações Importantes

### String de Conexão

A string de conexão está configurada no arquivo `appsettings.json` da API:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProvaTecnicaDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### CORS

A API está configurada para aceitar requisições de qualquer origem para facilitar o desenvolvimento:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

### URLs da API

- WebForms: Configurado em `web.config` - `ApiBaseUrl = "http://localhost:5000/api"`
- Angular: Configurado em `funcionario.service.ts` - `apiUrl = "http://localhost:5000/api"`

## Notas de Atualização

### Versão 2.0
- Atualização para .NET 8.0
- Atualização do Entity Framework Core para 8.0
- Atualização do QuestPDF para versão 2023.12.6
- Melhorias na geração de PDFs
- Otimizações de performance
- Correções de bugs

## Testando a Aplicação

### 1. Testar a API
1. Execute a API (`dotnet run` no diretório Api)
2. Acesse http://localhost:5000/swagger
3. Teste os endpoints usando a interface Swagger

### 2. Testar o WebForms
1. Execute a API
2. Execute o WebForms (`dotnet run` no diretório WebFormsUI)
3. Acesse http://localhost:5002
4. Navegue para as páginas Funcionários e Férias
5. Teste as operações CRUD e geração de PDF

### 3. Testar o Angular
1. Execute a API
2. Execute o Angular (`npm start` no diretório AngularUI)
3. Acesse http://localhost:4200
4. Teste as operações CRUD e geração de PDF

## Funcionalidades Especiais

### Histórico de Alterações
Quando um funcionário é atualizado via PUT, o sistema automaticamente registra as alterações na tabela `HistoricoAlteracao`, comparando os valores antigos com os novos.

### Geração de PDF
O sistema gera relatórios em PDF com informações dos funcionários, incluindo:
- Nome, cargo, data de admissão e salário
- Status das férias (Pendente, Em andamento, Concluída)

### Status de Férias
O status das férias é calculado automaticamente baseado na data atual:
- **Pendente**: Data atual anterior à data de início
- **Em andamento**: Data atual entre data de início e fim
- **Concluída**: Data atual posterior à data de fim

## Estrutura de Arquivos

```
ProvaTecnicaCSharp/
├── ProvaTecnicaCSharp.sln
├── README.md
├── Database/
│   ├── CreateTables.sql
│   └── SeedData.sql
├── Api/
│   ├── Api.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── Controllers/
│   ├── Data/
│   ├── Models/
│   ├── Services/
│   └── Properties/
├── WebFormsUI/
│   ├── WebFormsUI.csproj
│   ├── web.config
│   ├── Site.Master
│   ├── Default.aspx
│   ├── Funcionarios.aspx
│   ├── Ferias.aspx
│   ├── Helpers/
│   └── Models/
└── AngularUI/
    ├── package.json
    ├── angular.json
    ├── tsconfig.json
    └── src/
        └── app/
            ├── components/
            ├── models/
            └── services/
```

## Troubleshooting

### Problemas Comuns

1. **Erro de conexão com banco de dados**
   - Verifique se o SQL Server LocalDB está instalado
   - Execute os scripts de criação do banco

2. **CORS errors no Angular**
   - Verifique se a API está rodando
   - Confirme a URL da API no serviço Angular

3. **Erro 404 no WebForms**
   - Verifique se a API está rodando na porta 5000
   - Confirme a configuração no web.config

4. **Pacotes NuGet não encontrados**
   - Execute `dotnet restore` nos projetos C#
   - Execute `npm install` no projeto Angular

## Autor

Projeto desenvolvido como parte da Prova Técnica Prática para Desenvolvedor C# Júnior.

## Licença

Este projeto é apenas para fins educacionais e de avaliação técnica.

