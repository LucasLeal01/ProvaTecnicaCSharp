<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsUI.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1 class="display-4">Prova Técnica - Desenvolvedor C# Júnior</h1>
        <p class="lead">Sistema de gerenciamento de funcionários e férias.</p>
        <hr class="my-4" />
        <p>Use os links de navegação para acessar as funcionalidades do sistema.</p>
        <div class="row">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Funcionários</h5>
                        <p class="card-text">Gerencie os funcionários da empresa.</p>
                        <a href="Funcionarios.aspx" class="btn btn-primary">Acessar</a>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Férias</h5>
                        <p class="card-text">Gerencie as férias dos funcionários.</p>
                        <a href="Ferias.aspx" class="btn btn-primary">Acessar</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

