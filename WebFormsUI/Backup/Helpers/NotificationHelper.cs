using System;
using System.Web.UI;

namespace WebFormsUI.Helpers
{
    public static class NotificationHelper
    {
        public static void ShowSuccess(Page page, string message)
        {
            var script = $@"
                showNotification('{message}', 'success');
            ";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "success", script, true);
        }

        public static void ShowError(Page page, string message)
        {
            var script = $@"
                showNotification('{message}', 'error');
            ";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "error", script, true);
        }

        public static void ShowWarning(Page page, string message)
        {
            var script = $@"
                showNotification('{message}', 'warning');
            ";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "warning", script, true);
        }

        public static void ShowInfo(Page page, string message)
        {
            var script = $@"
                showNotification('{message}', 'info');
            ";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "info", script, true);
        }
    }
} 