using System;
using System.Collections.Generic;
using System.Text;

namespace ReplicaLibrary.Services
{
    public interface ILogService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogError(Exception exception, string message);
    }
}
