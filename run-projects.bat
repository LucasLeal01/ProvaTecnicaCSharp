@echo off
echo ========================================
echo Prova Tecnica C# - Script de Execucao
echo ========================================
echo.

echo 1. Executando API...
start "API" cmd /k "cd Api && dotnet run"
timeout /t 5

echo 2. Executando WebForms...
start "WebForms" cmd /k "cd WebFormsUI && dotnet run"
timeout /t 3

echo 3. Executando Angular (opcional)...
echo Para executar o Angular, abra um novo terminal e execute:
echo cd AngularUI
echo npm install
echo npm start
echo.

echo ========================================
echo Projetos iniciados!
echo API: http://localhost:5000
echo WebForms: http://localhost:5002
echo Angular: http://localhost:4200 (apos npm start)
echo ========================================
pause

