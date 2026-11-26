using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.Services
{
    public class LogService(ILogger<LogService> logService) : ILogService
    {
        private static readonly string logPath = "exceptions.log";

        public void LogError(string message)
        {
            logService.LogError(message);
        }

        public void LogError(Exception exception, string message)
        {
            logService.LogError(exception, message);
        }

        public void LogInformation(string message)
        {
            logService.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            logService.LogWarning(message);
        }

        public async Task LogExceptionAsync(Exception exception)
        {
            try
            {
                using (StreamWriter streamWriter = new(logPath, true, Encoding.UTF8, 8192))
                {
                    await streamWriter.WriteLineAsync($"Ошибка: {exception.ToString()}\n\t[{DateTime.UtcNow.Date:hh:mm:ss dd/MM/yyyy}]\n\n");
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Файл {logPath} не найден: " + ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
