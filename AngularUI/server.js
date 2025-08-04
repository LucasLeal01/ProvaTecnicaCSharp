const express = require('express');
const path = require('path');
const { createProxyMiddleware } = require('http-proxy-middleware');
const app = express();

// Verifica se o diretório dist/angular-ui existe
const fs = require('fs');
const distPath = path.join(__dirname, 'dist/angular-ui');

if (!fs.existsSync(distPath)) {
  console.error(`Erro: O diretório ${distPath} não existe!`);
  console.log('Conteúdo do diretório atual:');
  console.log(fs.readdirSync(__dirname));
  
  // Verifica se existe um diretório dist
  const parentDistPath = path.join(__dirname, 'dist');
  if (fs.existsSync(parentDistPath)) {
    console.log('Conteúdo do diretório dist:');
    console.log(fs.readdirSync(parentDistPath));
  }
}

// Configuração do proxy para a API
const apiUrl = process.env.API_URL || 'http://localhost:5000';
console.log(`Configurando proxy para API: ${apiUrl}`);

// Adiciona o proxy para a API
app.use('/api', createProxyMiddleware({
  target: apiUrl,
  changeOrigin: true,
  secure: false,
  logLevel: 'debug',
  pathRewrite: { '^/api': '/api' },
  onProxyReq: (proxyReq, req, res) => {
    console.log(`Proxy request: ${req.method} ${req.url} -> ${apiUrl}${req.url}`);
    // Adiciona cabeçalhos para debug
    proxyReq.setHeader('X-Forwarded-Host', req.headers.host);
    proxyReq.setHeader('X-Forwarded-Proto', 'https');
  },
  onProxyRes: (proxyRes, req, res) => {
    console.log(`Proxy response: ${proxyRes.statusCode} for ${req.method} ${req.url}`);
  },
  onError: (err, req, res) => {
    console.error('Erro no proxy:', err);
    res.status(500).json({ error: 'Erro ao conectar com a API', details: err.message });
  }
}));

// Serve os arquivos estáticos da pasta dist
app.use(express.static(distPath));

// Redireciona todas as requisições para o index.html
app.get('/*', function(req, res) {
  res.sendFile(path.join(distPath, 'index.html'));
});

// Inicia o servidor na porta definida pelo ambiente ou na 4200
const port = process.env.PORT || 10000;
app.listen(port, () => {
  console.log(`Servidor iniciado na porta ${port}`);
  console.log(`Servindo arquivos de ${distPath}`);
});