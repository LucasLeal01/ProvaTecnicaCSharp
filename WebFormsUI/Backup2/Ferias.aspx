<%@ Page Title="F&eacute;rias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ferias.aspx.cs" Inherits="WebFormsUI.Ferias" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12">
            <h2>Gerenciamento de F&eacute;rias</h2>
            
            <div class="card mb-4">
                <div class="card-header">
                    <h5>Cadastrar F&eacute;rias</h5>
                </div>
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="ddlFuncionario" class="form-label">Funcion&aacute;rio:</label>
                                    <asp:DropDownList ID="ddlFuncionario" runat="server" CssClass="form-select" 
                                        DataTextField="Nome" DataValueField="Id">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtDataInicio" class="form-label">Data In&iacute;cio:</label>
                                    <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtDataFim" class="form-label">Data Fim:</label>
                                    <asp:TextBox ID="txtDataFim" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label class="form-label">&nbsp;</label>
                                    <div>
                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
                                    </div>
                                </div>
                            </div>
                            
                            <asp:Label ID="lblMensagem" runat="server" CssClass="alert alert-info mt-3" Visible="false"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="card">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5>Lista de F&eacute;rias</h5>
                    <asp:Button ID="btnRelatorio" runat="server" Text="Gerar Relat&oacute;rio PDF" CssClass="btn btn-success" OnClick="btnRelatorio_Click" />
                </div>
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvFerias" runat="server" CssClass="table table-striped" 
                                AutoGenerateColumns="false" DataKeyNames="Id" 
                                OnRowDeleting="gvFerias_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="ID" />
                                    <asp:TemplateField HeaderText="Funcion&aacute;rio">
                                        <ItemTemplate>
                                            <%# Eval("Funcionario.Nome") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DataInicio" HeaderText="Data In&iacute;cio" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="DataFim" HeaderText="Data Fim" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="A&ccedil;&otilde;es">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkExcluir" runat="server" CommandName="Delete" 
                                                CssClass="btn btn-sm btn-danger" 
                                                OnClientClick="return confirm('Deseja realmente excluir esta f&eacute;rias?');">
                                                Excluir
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-info">Nenhuma f&eacute;rias encontrada.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

