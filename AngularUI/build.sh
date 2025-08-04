#!/bin/bash

# Instalar dependências
npm install

# Construir a aplicação Angular
npm run build

# Garantir que o server.js tenha permissão de execução
chmod +x server.js