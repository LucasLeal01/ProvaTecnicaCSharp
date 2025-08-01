using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<HistoricoAlteracao> HistoricoAlteracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.ToTable("Funcionario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Cargo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DataAdmissao).IsRequired();
                entity.Property(e => e.Salario).IsRequired().HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Ferias>(entity =>
            {
                entity.ToTable("Ferias");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FuncionarioId).IsRequired();
                entity.Property(e => e.DataInicio).IsRequired();
                entity.Property(e => e.DataFim).IsRequired();
                
                entity.HasOne(e => e.Funcionario)
                      .WithMany(f => f.Ferias)
                      .HasForeignKey(e => e.FuncionarioId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HistoricoAlteracao>(entity =>
            {
                entity.ToTable("HistoricoAlteracao");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Entidade).IsRequired().HasMaxLength(50);
                entity.Property(e => e.EntidadeId).IsRequired();
                entity.Property(e => e.Campo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DataHora).IsRequired().HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Funcionario>().HasData(
                new Funcionario { Id = 1, Nome = "João Silva", Cargo = "Desenvolvedor Júnior", DataAdmissao = new DateTime(2023, 1, 15), Salario = 4500.00m },
                new Funcionario { Id = 2, Nome = "Maria Santos", Cargo = "Analista de Sistemas", DataAdmissao = new DateTime(2022, 6, 10), Salario = 6200.00m },
                new Funcionario { Id = 3, Nome = "Pedro Oliveira", Cargo = "Desenvolvedor Sênior", DataAdmissao = new DateTime(2021, 3, 20), Salario = 8500.00m }
            );

            modelBuilder.Entity<Ferias>().HasData(
                new Ferias { Id = 1, FuncionarioId = 1, DataInicio = new DateTime(2023, 12, 20), DataFim = new DateTime(2024, 1, 10) },
                new Ferias { Id = 2, FuncionarioId = 2, DataInicio = new DateTime(2024, 7, 1), DataFim = new DateTime(2024, 7, 20) },
                new Ferias { Id = 3, FuncionarioId = 3, DataInicio = new DateTime(2024, 12, 15), DataFim = new DateTime(2024, 12, 30) }
            );
        }
    }
}

