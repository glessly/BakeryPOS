using System;
using System.IO;

namespace BakeryPOS
{
    public static class ErrorLogger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        public static void LogError(string context, Exception ex)
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                string logFile = Path.Combine(LogDirectory, $"ErrorLog_{DateTime.Now:yyyy-MM-dd}.txt");
                string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{context}] {ex.GetType().Name}: {ex.Message}\r\n{ex.StackTrace}\r\n--------------------------------------------------\r\n";
                File.AppendAllText(logFile, entry);
            }
            catch
            {
                // Never crash if file logging fails
            }
        }
    }
}