using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebFormsUI.Helpers;
using WebFormsUI.Models;

namespace WebFormsUI
{
    public partial class Ferias : Page
    {
        protected async Task Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await CarregarFuncionarios();
                await CarregarFerias();
            }
        }

        private async Task CarregarFuncionarios()
        {
            try
            {
                var funcionarios = await ApiHelper.GetAsync<List<Funcionario>>("funcionarios");
                ddlFuncionario.DataSource = funcionarios;
                ddlFuncionario.DataBind();
                ddlFuncionario.Items.Insert(0, new ListItem("Selecione um funcionário", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao carregar funcionários: {0}", ex.Message), "alert-danger");
            }
        }

        private async Task CarregarFerias()
        {
            try
            {
                var ferias = await ApiHelper.GetAsync<List<Models.Ferias>>("ferias");
                gvFerias.DataSource = ferias;
                gvFerias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao carregar férias: {0}", ex.Message), "alert-danger");
            }
        }

        protected async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlFuncionario.SelectedValue == "0")
                {
                    MostrarMensagem("Selecione um funcionário.", "alert-warning");
                    return;
                }

                if (string.IsNullOrEmpty(txtDataInicio.Text) || string.IsNullOrEmpty(txtDataFim.Text))
                {
                    MostrarMensagem("Preencha as datas de início e fim.", "alert-warning");
                    return;
                }

                var dataInicio = Convert.ToDateTime(txtDataInicio.Text);
                var dataFim = Convert.ToDateTime(txtDataFim.Text);

                if (dataFim <= dataInicio)
                {
                    MostrarMensagem("A data de fim deve ser posterior à data de início.", "alert-warning");
                    return;
                }

                var ferias = new Models.Ferias
                {
                    FuncionarioId = Convert.ToInt32(ddlFuncionario.SelectedValue),
                    DataInicio = dataInicio,
                    DataFim = dataFim
                };

                await ApiHelper.PostAsync<Models.Ferias>("ferias", ferias);
                
                ddlFuncionario.SelectedIndex = 0;
                txtDataInicio.Text = string.Empty;
                txtDataFim.Text = string.Empty;
                
                await CarregarFerias();
                MostrarMensagem("Férias cadastradas com sucesso!", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao salvar férias: {0}", ex.Message), "alert-danger");
            }
        }

        protected async void gvFerias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(gvFerias.DataKeys[e.RowIndex].Value);
                await ApiHelper.DeleteAsync(string.Format("ferias/{0}", id));
                await CarregarFerias();
                MostrarMensagem("Férias excluídas com sucesso!", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao excluir férias: {0}", ex.Message), "alert-danger");
            }
        }

        protected async void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                var pdfBytes = await ApiHelper.GetPdfAsync("funcionarios/relatorio/pdf");
                
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=relatorio-funcionarios.pdf");
                Response.BinaryWrite(pdfBytes);
                Response.End();
            }
            catch (Exception ex)
            {
                MostrarMensagem(string.Format("Erro ao gerar relatório: {0}", ex.Message), "alert-danger");
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

