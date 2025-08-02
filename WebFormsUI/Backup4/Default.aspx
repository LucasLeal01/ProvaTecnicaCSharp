<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsUI.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Sistema de Gerenciamento</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row">
                <div class="col-12">
                    <h1 class="text-center mb-4">Sistema de Gerenciamento</h1>
                    
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">Funcion&aacute;rios</h5>
                                    <p class="card-text">Gerencie o cadastro de funcion&aacute;rios, incluindo cria&ccedil;&atilde;o, edi&ccedil;&atilde;o e exclus&atilde;o de registros.</p>
                                    <a href="Funcionarios.aspx" class="btn btn-primary">Acessar</a>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col-md-6">
                            <div class="card">
                                <div class="card-body">
                                    <h5 class="card-title">F&eacute;rias</h5>
                                    <p class="card-text">Gerencie as f&eacute;rias dos funcion&aacute;rios, cadastrando per&iacute;odos de f&eacute;rias.</p>
                                    <a href="Ferias.aspx" class="btn btn-success">Acessar</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    
    <script src="Scripts/bootstrap.bundle.min.js"></script>
</body>
</html>
