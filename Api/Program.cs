using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api.Data;
using Api.Services;
using Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Em produção, usar a variável de ambiente DATABASE_URL se disponível
if (builder.Environment.IsProduction())
{
    string dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (!string.IsNullOrEmpty(dbUrl))
    {
        connectionString = dbUrl;
    }
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IHistoricoService, HistoricoService>();
builder.Services.AddSingleton<ILoggingService, LoggingService>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // Permitir qualquer origem, método e cabeçalho
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
    
    try
    {
        logger.LogInformation("Iniciando configuração do banco de dados");
        loggingService.LogInfo("Iniciando configuração do banco de dados");
        
        // Verifica a string de conexão
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var dbUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        logger.LogInformation($"String de conexão configurada: {(!string.IsNullOrEmpty(dbUrl) ? "Usando DATABASE_URL" : "Usando DefaultConnection")}");
        loggingService.LogInfo($"String de conexão configurada: {(!string.IsNullOrEmpty(dbUrl) ? "Usando DATABASE_URL" : "Usando DefaultConnection")}");
        
        // Garante que o banco de dados seja criado e migrado
        logger.LogInformation("Criando banco de dados se não existir");
        loggingService.LogInfo("Criando banco de dados se não existir");
        context.Database.EnsureCreated();
        logger.LogInformation("Banco de dados verificado/criado com sucesso");
        loggingService.LogInfo("Banco de dados verificado/criado com sucesso");
        
        // Verifica se existem funcionários no banco
        var funcionariosCount = context.Funcionarios.Count();
        logger.LogInformation($"Número de funcionários encontrados: {funcionariosCount}");
        loggingService.LogInfo($"Número de funcionários encontrados: {funcionariosCount}");
        
        if (funcionariosCount == 0)
        {
            logger.LogInformation("Adicionando dados de exemplo ao banco de dados");
            loggingService.LogInfo("Adicionando dados de exemplo ao banco de dados");
            
            // Adiciona dados de exemplo se o banco estiver vazio
            context.Funcionarios.AddRange(
                new Funcionario { Id = 1, Nome = "João Silva", Cargo = "Desenvolvedor Júnior", DataAdmissao = new DateTime(2023, 1, 15), Salario = 4500.00m },
                new Funcionario { Id = 2, Nome = "Maria Santos", Cargo = "Analista de Sistemas", DataAdmissao = new DateTime(2022, 6, 10), Salario = 6200.00m },
                new Funcionario { Id = 3, Nome = "Pedro Oliveira", Cargo = "Desenvolvedor Sênior", DataAdmissao = new DateTime(2021, 3, 20), Salario = 8500.00m }
            );
            
            context.Ferias.AddRange(
                new Ferias { Id = 1, FuncionarioId = 1, DataInicio = new DateTime(2023, 12, 20), DataFim = new DateTime(2024, 1, 10) },
                new Ferias { Id = 2, FuncionarioId = 2, DataInicio = new DateTime(2024, 7, 1), DataFim = new DateTime(2024, 7, 20) },
                new Ferias { Id = 3, FuncionarioId = 3, DataInicio = new DateTime(2024, 12, 15), DataFim = new DateTime(2024, 12, 30) }
            );
            
            context.SaveChanges();
            logger.LogInformation("Dados de exemplo adicionados com sucesso");
            loggingService.LogInfo("Dados de exemplo adicionados com sucesso");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocorreu um erro ao inicializar o banco de dados.");
        loggingService.LogError($"Ocorreu um erro ao inicializar o banco de dados: {ex.Message}");
        
        // Registra detalhes adicionais do erro
        if (ex.InnerException != null)
        {
            logger.LogError(ex.InnerException, "Erro interno:");
            loggingService.LogError($"Erro interno: {ex.InnerException.Message}");
        }
    }
}

app.Run();

