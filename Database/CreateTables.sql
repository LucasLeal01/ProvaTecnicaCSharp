-- Script de criação das tabelas para Prova Técnica C# Júnior
-- SQL Server LocalDB

USE master;
GO

-- Criar banco de dados se não existir
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'ProvaTecnicaDB')
BEGIN
    CREATE DATABASE ProvaTecnicaDB;
END
GO

USE ProvaTecnicaDB;
GO

-- Tabela Funcionario
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Funcionario' AND xtype='U')
BEGIN
    CREATE TABLE Funcionario (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nome NVARCHAR(100) NOT NULL,
        Cargo NVARCHAR(50) NOT NULL,
        DataAdmissao DATE NOT NULL,
        Salario DECIMAL(10,2) NOT NULL
    );
END
GO

-- Tabela Ferias
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Ferias' AND xtype='U')
BEGIN
    CREATE TABLE Ferias (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FuncionarioId INT NOT NULL,
        DataInicio DATE NOT NULL,
        DataFim DATE NOT NULL,
        FOREIGN KEY (FuncionarioId) REFERENCES Funcionario(Id) ON DELETE CASCADE
    );
END
GO

-- Tabela HistoricoAlteracao
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='HistoricoAlteracao' AND xtype='U')
BEGIN
    CREATE TABLE HistoricoAlteracao (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Entidade NVARCHAR(50) NOT NULL,
        EntidadeId INT NOT NULL,
        Campo NVARCHAR(50) NOT NULL,
        ValorAntigo NVARCHAR(MAX),
        ValorNovo NVARCHAR(MAX),
        DataHora DATETIME2 NOT NULL DEFAULT GETDATE()
    );
END
GO

PRINT 'Tabelas criadas com sucesso!';

