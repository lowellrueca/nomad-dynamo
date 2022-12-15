using System.Diagnostics;

namespace Nomad.Settings
{
    static class AppSettings
    {
        public static string LoggerName { get => "Logger"; }
        public static string LoggerUrl
        {
            get
            {
                return @"C:\Users\lowell.rueca\source\repos\nomad-project\nomad-logs\logs.txt";
            }
        }

        public static void InitTraceDiagnostic()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(AppSettings.LoggerUrl, AppSettings.LoggerName));
        }
    }
}