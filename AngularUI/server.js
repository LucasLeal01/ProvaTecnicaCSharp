const express = require('express');
const path = require('path');
const app = express();

// Serve os arquivos estáticos da pasta dist
app.use(express.static(path.join(__dirname, 'dist/angular-ui')));

// Redireciona todas as requisições para o index.html
app.get('/*', function(req, res) {
  res.sendFile(path.join(__dirname, 'dist/angular-ui/index.html'));
});

// Inicia o servidor na porta definida pelo ambiente ou na 4200
const port = process.env.PORT || 4200;
app.listen(port, () => {
  console.log(`Servidor iniciado na porta ${port}`);
});