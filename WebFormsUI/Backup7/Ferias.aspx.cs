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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    await CarregarFuncionarios();
                    await CarregarFerias();
                }));
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
                LoggingHelper.LogError("Erro ao carregar funcionários na página de férias", ex);
                NotificationHelper.ShowError(this, "Erro ao carregar funcionários. Tente novamente.");
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
                LoggingHelper.LogError("Erro ao carregar férias", ex);
                NotificationHelper.ShowError(this, "Erro ao carregar férias. Tente novamente.");
            }
        }

        protected async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlFuncionario.SelectedValue == "0")
                {
                    NotificationHelper.ShowWarning(this, "Selecione um funcionário.");
                    return;
                }

                if (string.IsNullOrEmpty(txtDataInicio.Text) || string.IsNullOrEmpty(txtDataFim.Text))
                {
                    NotificationHelper.ShowWarning(this, "Preencha as datas de início e fim.");
                    return;
                }

                var dataInicio = Convert.ToDateTime(txtDataInicio.Text);
                var dataFim = Convert.ToDateTime(txtDataFim.Text);

                if (dataFim <= dataInicio)
                {
                    NotificationHelper.ShowWarning(this, "A data de fim deve ser posterior à data de início.");
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
                LoggingHelper.LogInformation($"Férias cadastradas com sucesso: Funcionário ID {ferias.FuncionarioId}, Período: {dataInicio:dd/MM/yyyy} a {dataFim:dd/MM/yyyy}");
                NotificationHelper.ShowSuccess(this, "Férias cadastradas com sucesso!");
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError("Erro ao salvar férias", ex);
                NotificationHelper.ShowError(this, "Erro ao salvar férias. Tente novamente.");
            }
        }

        protected async void gvFerias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = Convert.ToInt32(gvFerias.DataKeys[e.RowIndex].Value);
            
            try
            {
                await ApiHelper.DeleteAsync(string.Format("ferias/{0}", id));
                await CarregarFerias();
                LoggingHelper.LogInformation($"Férias excluídas com sucesso: ID {id}");
                NotificationHelper.ShowSuccess(this, "Férias excluídas com sucesso!");
            }
            catch (Exception ex)
            {
                LoggingHelper.LogError($"Erro ao excluir férias ID: {id}", ex);
                NotificationHelper.ShowError(this, "Erro ao excluir férias. Tente novamente.");
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
                LoggingHelper.LogError("Erro ao gerar relatório PDF de férias", ex);
                NotificationHelper.ShowError(this, "Erro ao gerar relatório. Tente novamente.");
            }
        }


    }
}

