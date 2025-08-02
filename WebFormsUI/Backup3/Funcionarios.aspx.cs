using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsUI.Helpers;
using WebFormsUI.Models;

namespace WebFormsUI
{
    public partial class Funcionarios : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    await CarregarFuncionarios();
                }));
            }
        }

        private async Task CarregarFuncionarios()
        {
            try
            {
                var funcionarios = await ApiHelper.GetAsync<List<Funcionario>>("funcionarios");
                gvFuncionarios.DataSource = funcionarios;
                gvFuncionarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao carregar funcionários: {0}", ex.Message), "alert-danger");
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            dvFuncionario.ChangeMode(DetailsViewMode.Insert);
            dvFuncionario.Visible = true;
            gvFuncionarios.Visible = false;
        }

        protected async void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                var pdfBytes = await ApiHelper.GetPdfAsync("funcionarios/relatorio/pdf");

                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader(
                    "Content-Disposition",
                    "attachment; filename=relatorio-funcionarios.pdf"
                );
                Response.BinaryWrite(pdfBytes);
                Response.End();
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao gerar relatório: {0}", ex.Message), "alert-danger");
            }
        }

        protected async void gvFuncionarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFuncionarios.EditIndex = e.NewEditIndex;
            await CarregarFuncionarios();
        }

        protected async void gvFuncionarios_RowCancelingEdit(
            object sender,
            GridViewCancelEditEventArgs e
        )
        {
            gvFuncionarios.EditIndex = -1;
            await CarregarFuncionarios();
        }

        protected async void gvFuncionarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var row = gvFuncionarios.Rows[e.RowIndex];
                var id = Convert.ToInt32(gvFuncionarios.DataKeys[e.RowIndex].Value);

                var funcionario = new Funcionario
                {
                    Id = id,
                    Nome = ((TextBox)row.Cells[1].Controls[0]).Text,
                    Cargo = ((TextBox)row.Cells[2].Controls[0]).Text,
                    DataAdmissao = Convert.ToDateTime(((TextBox)row.Cells[3].Controls[0]).Text),
                    Salario = Convert.ToDecimal(((TextBox)row.Cells[4].Controls[0]).Text),
                };

                await ApiHelper.PutAsync(string.Format("funcionarios/{0}", id), funcionario);

                gvFuncionarios.EditIndex = -1;
                await CarregarFuncionarios();
                MostrarMensagem("Funcionário atualizado com sucesso!", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao atualizar funcionário: {0}", ex.Message), "alert-danger");
            }
        }

        protected async void gvFuncionarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(gvFuncionarios.DataKeys[e.RowIndex].Value);
                await ApiHelper.DeleteAsync(string.Format("funcionarios/{0}", id));
                await CarregarFuncionarios();
                MostrarMensagem("Funcionário excluído com sucesso!", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao excluir funcionário: {0}", ex.Message), "alert-danger");
            }
        }

        protected async void dvFuncionario_ItemInserting(
            object sender,
            DetailsViewInsertEventArgs e
        )
        {
            try
            {
                var funcionario = new Funcionario
                {
                    Nome = e.Values["Nome"].ToString(),
                    Cargo = e.Values["Cargo"].ToString(),
                    DataAdmissao = Convert.ToDateTime(e.Values["DataAdmissao"]),
                    Salario = Convert.ToDecimal(e.Values["Salario"]),
                };

                await ApiHelper.PostAsync<Funcionario>("funcionarios", funcionario);

                dvFuncionario.Visible = false;
                gvFuncionarios.Visible = true;
                await CarregarFuncionarios();
                MostrarMensagem("Funcionário criado com sucesso!", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao criar funcionário: {0}", ex.Message), "alert-danger");
                e.Cancel = true;
            }
        }

        protected async void dvFuncionario_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
        }

        protected async void dvFuncionario_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            if (e.NewMode == DetailsViewMode.ReadOnly)
            {
                dvFuncionario.Visible = false;
                gvFuncionarios.Visible = true;
                await CarregarFuncionarios();
            }
        }

        private void MostrarMensagem(string mensagem, string cssClass)
        {
            lblMensagem.Text = mensagem;
            lblMensagem.CssClass = string.Format("alert {0}", cssClass);
            lblMensagem.Visible = true;
        }
    }
}
