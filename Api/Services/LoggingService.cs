using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Api.Services
{
    public interface ILoggingService
    {
        void LogInformation(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? exception = null);
    }

    public class LoggingService : ILoggingService
    {
        private readonly string _logFilePath;
        private readonly object _lockObject = new object();

        public LoggingService()
        {
            var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            _logFilePath = Path.Combine(logDirectory, $"application_{DateTime.Now:yyyyMMdd}.log");
        }

        public void LogInformation(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public void LogError(string message, Exception? exception = null)
        {
            var fullMessage = message;
            if (exception != null)
            {
                fullMessage += $"\nException: {exception.Message}\nStackTrace: {exception.StackTrace}";
            }
            WriteLog("ERROR", fullMessage);
        }

        private void WriteLog(string level, string message)
        {
            lock (_lockObject)
            {
                var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
            }
        }
    }
}