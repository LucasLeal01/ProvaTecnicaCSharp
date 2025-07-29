-- Script de dados de seed para Prova Técnica C# Júnior
-- SQL Server LocalDB

USE ProvaTecnicaDB;
GO

-- Limpar dados existentes (opcional)
DELETE FROM Ferias;
DELETE FROM HistoricoAlteracao;
DELETE FROM Funcionario;
GO

-- Inserir funcionários de exemplo
INSERT INTO Funcionario (Nome, Cargo, DataAdmissao, Salario) VALUES
('João Silva', 'Desenvolvedor Júnior', '2023-01-15', 4500.00),
('Maria Santos', 'Analista de Sistemas', '2022-06-10', 6200.00),
('Pedro Oliveira', 'Desenvolvedor Sênior', '2021-03-20', 8500.00);
GO

-- Inserir algumas férias de exemplo
INSERT INTO Ferias (FuncionarioId, DataInicio, DataFim) VALUES
(1, '2023-12-20', '2024-01-10'),  -- João - Férias concluídas
(2, '2024-07-01', '2024-07-20'),  -- Maria - Férias em andamento (dependendo da data atual)
(3, '2024-12-15', '2024-12-30');  -- Pedro - Férias futuras
GO

-- Inserir alguns registros de histórico de exemplo
INSERT INTO HistoricoAlteracao (Entidade, EntidadeId, Campo, ValorAntigo, ValorNovo, DataHora) VALUES
('Funcionario', 1, 'Salario', '4000.00', '4500.00', '2023-06-15 10:30:00'),
('Funcionario', 2, 'Cargo', 'Desenvolvedora Júnior', 'Analista de Sistemas', '2023-01-10 14:20:00'),
('Funcionario', 3, 'Salario', '8000.00', '8500.00', '2023-12-01 09:15:00');
GO

PRINT 'Dados de seed inseridos com sucesso!';

-- Verificar dados inseridos
SELECT 'Funcionarios' as Tabela, COUNT(*) as Total FROM Funcionario
UNION ALL
SELECT 'Ferias', COUNT(*) FROM Ferias
UNION ALL
SELECT 'HistoricoAlteracao', COUNT(*) FROM HistoricoAlteracao;
GO

