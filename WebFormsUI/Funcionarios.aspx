<%@ Page Title="Funcion&aacute;rios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Funcionarios.aspx.cs" Inherits="WebFormsUI.Funcionarios" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12">
            <h2>Gerenciamento de Funcion&aacute;rios</h2>
            
            <div class="mb-3">
                <asp:Button ID="btnNovo" runat="server" Text="Novo Funcion&aacute;rio" CssClass="btn btn-primary" OnClick="btnNovo_Click" />
                <asp:Button ID="btnRelatorio" runat="server" Text="Gerar Relat&oacute;rio PDF" CssClass="btn btn-success" OnClick="btnRelatorio_Click" />
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvFuncionarios" runat="server" CssClass="table table-striped" 
                        AutoGenerateColumns="false" DataKeyNames="Id" 
                        OnRowEditing="gvFuncionarios_RowEditing" 
                        OnRowDeleting="gvFuncionarios_RowDeleting"
                        OnRowCancelingEdit="gvFuncionarios_RowCancelingEdit"
                        OnRowUpdating="gvFuncionarios_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                            <asp:BoundField DataField="DataAdmissao" HeaderText="Data Admiss&atilde;o" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="Salario" HeaderText="Sal&aacute;rio" DataFormatString="{0:C}" />
                            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">Nenhum funcion&aacute;rio encontrado.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:DetailsView ID="dvFuncionario" runat="server" CssClass="table table-bordered" 
                        AutoGenerateRows="false" DataKeyNames="Id" Visible="false"
                        OnItemInserting="dvFuncionario_ItemInserting"
                        OnItemUpdating="dvFuncionario_ItemUpdating"
                        OnModeChanging="dvFuncionario_ModeChanging">
                        <Fields>
                            <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" InsertVisible="false" />
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                            <asp:BoundField DataField="DataAdmissao" HeaderText="Data Admiss&atilde;o" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false" />
                            <asp:BoundField DataField="Salario" HeaderText="Sal&aacute;rio" />
                            <asp:CommandField ShowInsertButton="true" ShowCancelButton="true" />
                        </Fields>
                    </asp:DetailsView>

                    <asp:Label ID="lblMensagem" runat="server" CssClass="alert alert-info" Visible="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

