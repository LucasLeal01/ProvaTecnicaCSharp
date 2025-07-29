#!/bin/bash

echo "========================================"
echo "Prova Tecnica C# - Script de Execucao"
echo "========================================"
echo

echo "1. Executando API..."
cd Api
gnome-terminal -- bash -c "dotnet run; exec bash" 2>/dev/null || \
xterm -e "dotnet run" 2>/dev/null || \
osascript -e 'tell app "Terminal" to do script "cd '$(pwd)' && dotnet run"' 2>/dev/null || \
echo "Execute manualmente: cd Api && dotnet run"
cd ..
sleep 5

echo "2. Executando WebForms..."
cd WebFormsUI
gnome-terminal -- bash -c "dotnet run; exec bash" 2>/dev/null || \
xterm -e "dotnet run" 2>/dev/null || \
osascript -e 'tell app "Terminal" to do script "cd '$(pwd)' && dotnet run"' 2>/dev/null || \
echo "Execute manualmente: cd WebFormsUI && dotnet run"
cd ..
sleep 3

echo "3. Para executar o Angular (opcional):"
echo "cd AngularUI"
echo "npm install"
echo "npm start"
echo

echo "========================================"
echo "Projetos iniciados!"
echo "API: http://localhost:5000"
echo "WebForms: http://localhost:5002"
echo "Angular: http://localhost:4200 (após npm start)"
echo "========================================"

