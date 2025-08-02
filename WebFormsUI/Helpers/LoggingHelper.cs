using System;
using System.IO;
using System.Web;

namespace WebFormsUI.Helpers
{
    public static class LoggingHelper
    {
        private static readonly string LogDirectory = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "Logs");
        private static readonly object LockObject = new object();

        static LoggingHelper()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }

        public static void LogInformation(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public static void LogError(string message, Exception exception = null)
        {
            var fullMessage = message;
            if (exception != null)
            {
                fullMessage += $"\nException: {exception.Message}\nStackTrace: {exception.StackTrace}";
            }
            WriteLog("ERROR", fullMessage);
        }

        private static void WriteLog(string level, string message)
        {
            lock (LockObject)
            {
                var logFilePath = Path.Combine(LogDirectory, $"webforms_{DateTime.Now:yyyyMMdd}.log");
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
        }
    }
} 