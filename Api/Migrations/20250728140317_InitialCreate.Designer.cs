﻿
using System;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250728140317_InitialCreate")]
    partial class InitialCreate
    {

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Api.Models.Ferias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("Ferias", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataFim = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataInicio = new DateTime(2023, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FuncionarioId = 1
                        },
                        new
                        {
                            Id = 2,
                            DataFim = new DateTime(2024, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataInicio = new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FuncionarioId = 2
                        },
                        new
                        {
                            Id = 3,
                            DataFim = new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataInicio = new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FuncionarioId = 3
                        });
                });

            modelBuilder.Entity("Api.Models.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DataAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("Funcionario", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cargo = "Desenvolvedor Júnior",
                            DataAdmissao = new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "João Silva",
                            Salario = 4500.00m
                        },
                        new
                        {
                            Id = 2,
                            Cargo = "Analista de Sistemas",
                            DataAdmissao = new DateTime(2022, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "Maria Santos",
                            Salario = 6200.00m
                        },
                        new
                        {
                            Id = 3,
                            Cargo = "Desenvolvedor Sênior",
                            DataAdmissao = new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "Pedro Oliveira",
                            Salario = 8500.00m
                        });
                });

            modelBuilder.Entity("Api.Models.HistoricoAlteracao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Campo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DataHora")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Entidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("EntidadeId")
                        .HasColumnType("int");

                    b.Property<string>("ValorAntigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValorNovo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HistoricoAlteracao", (string)null);
                });

            modelBuilder.Entity("Api.Models.Ferias", b =>
                {
                    b.HasOne("Api.Models.Funcionario", "Funcionario")
                        .WithMany("Ferias")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Api.Models.Funcionario", b =>
                {
                    b.Navigation("Ferias");
                });
#pragma warning restore 612, 618
        }
    }
}
