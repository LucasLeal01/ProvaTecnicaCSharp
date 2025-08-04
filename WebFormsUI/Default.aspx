<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsUI.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Sistema de Gerenciamento</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <style>
        :root {
            --primary-gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            --secondary-gradient: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            --success-gradient: linear-gradient(135deg, #4facfe 0%, #00f2fe 100%);
            --card-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            --card-hover-shadow: 0 20px 40px rgba(0, 0, 0, 0.15);
            --text-primary: #2d3748;
            --text-secondary: #718096;
        }

        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: var(--text-primary);
        }

        .main-container {
            min-height: 100vh;
            display: flex;
            align-items: center;
            padding: 2rem 0;
        }

        .page-title {
            color: white;
            font-size: 3rem;
            font-weight: 700;
            text-align: center;
            margin-bottom: 3rem;
            text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
            letter-spacing: -0.5px;
        }

        .page-subtitle {
            color: rgba(255, 255, 255, 0.9);
            text-align: center;
            font-size: 1.2rem;
            margin-bottom: 4rem;
            font-weight: 300;
        }

        .card-modern {
            background: white;
            border-radius: 20px;
            border: none;
            box-shadow: var(--card-shadow);
            transition: all 0.3s ease;
            overflow: hidden;
            position: relative;
            margin-bottom: 2rem;
        }

        .card-modern:hover {
            transform: translateY(-10px);
            box-shadow: var(--card-hover-shadow);
        }

        .card-modern::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            height: 4px;
            background: var(--primary-gradient);
        }

        .card-modern.success::before {
            background: var(--success-gradient);
        }

        .card-modern.secondary::before {
            background: var(--secondary-gradient);
        }

        .card-body-modern {
            padding: 2.5rem;
            text-align: center;
        }

        .card-icon {
            width: 80px;
            height: 80px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 1.5rem;
            font-size: 2rem;
            color: white;
            background: var(--primary-gradient);
        }

        .card-icon.success {
            background: var(--success-gradient);
        }

        .card-icon.secondary {
            background: var(--secondary-gradient);
        }

        .card-title-modern {
            font-size: 1.8rem;
            font-weight: 700;
            color: var(--text-primary);
            margin-bottom: 1rem;
        }

        .card-text-modern {
            color: var(--text-secondary);
            font-size: 1rem;
            line-height: 1.6;
            margin-bottom: 2rem;
        }

        .btn-modern {
            padding: 12px 30px;
            border-radius: 50px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            border: none;
            transition: all 0.3s ease;
            text-decoration: none;
            display: inline-block;
            position: relative;
            overflow: hidden;
        }

        .btn-modern::before {
            content: '';
            position: absolute;
            top: 0;
            left: -100%;
            width: 100%;
            height: 100%;
            background: linear-gradient(90deg, transparent, rgba(255, 255, 255, 0.2), transparent);
            transition: left 0.5s;
        }

        .btn-modern:hover::before {
            left: 100%;
        }

        .btn-primary-modern {
            background: var(--primary-gradient);
            color: white;
        }

        .btn-primary-modern:hover {
            background: linear-gradient(135deg, #5a6fd8 0%, #6a4190 100%);
            color: white;
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(102, 126, 234, 0.4);
        }

        .btn-success-modern {
            background: var(--success-gradient);
            color: white;
        }

        .btn-success-modern:hover {
            background: linear-gradient(135deg, #3e8bfe 0%, #00d4e6 100%);
            color: white;
            transform: translateY(-2px);
            box-shadow: 0 10px 20px rgba(79, 172, 254, 0.4);
        }

        .footer {
            text-align: center;
            color: rgba(255, 255, 255, 0.7);
            margin-top: 3rem;
            font-size: 0.9rem;
        }

        @media (max-width: 768px) {
            .page-title {
                font-size: 2rem;
            }
            
            .card-body-modern {
                padding: 2rem;
            }
            
            .card-icon {
                width: 60px;
                height: 60px;
                font-size: 1.5rem;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-container">
            <div class="container">
                <div class="row">
                    <div class="col-12">
                        <h1 class="page-title">Sistema de Gerenciamento</h1>
                        <p class="page-subtitle">Gerencie funcionarios e ferias de forma eficiente</p>
                        
                        <div class="row justify-content-center">
                            <div class="col-lg-5 col-md-6">
                                <div class="card-modern">
                                    <div class="card-body-modern">
                                        <div class="card-icon">
                                            <i class="fas fa-users"></i>
                                        </div>
                                        <h3 class="card-title-modern">Funcionarios</h3>
                                        <p class="card-text-modern">
                                            Gerencie o cadastro completo de funcionarios com recursos avancados de criacao, 
                                            edicao e exclusao de registros. Interface intuitiva e responsiva.
                                        </p>
                                        <a href="Funcionarios.aspx" class="btn-modern btn-primary-modern">
                                            <i class="fas fa-arrow-right me-2"></i>Acessar
                                        </a>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-lg-5 col-md-6">
                                <div class="card-modern success">
                                    <div class="card-body-modern">
                                        <div class="card-icon success">
                                            <i class="fas fa-umbrella-beach"></i>
                                        </div>
                                        <h3 class="card-title-modern">Ferias</h3>
                                        <p class="card-text-modern">
                                            Controle total sobre as ferias dos funcionarios. Cadastre periodos, 
                                            visualize historico e gerencie solicitacoes de forma organizada.
                                        </p>
                                        <a href="Ferias.aspx" class="btn-modern btn-success-modern">
                                            <i class="fas fa-arrow-right me-2"></i>Acessar
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="footer">
                            <p>&copy; 2025 Sistema de Gerenciamento. Todos os direitos reservados.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    
    <script src="Scripts/bootstrap.bundle.min.js"></script>
</body>
</html>
