using System;
using System.ComponentModel;
using System.Diagnostics;
using AL.Core.Models;
using AL.Core.Utilities;

namespace AL.Core.Loggers
{
    public class ProcessLogger : Logger<ProcessReport>
    {
        public override void Log()
        {
            var process = GetActiveProcess();

            try
            {
                var processReport = new ProcessReport
                {
                    Path = process.ProcessName,
                    Description = process.MainModule.FileVersionInfo.FileDescription,
                    Name = process.MainModule.ModuleName,
                    WindowTitle = process.MainWindowTitle,
                    FileName = process.MainModule.FileName
                };

                Observer.OnNext(processReport);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        private static Process GetActiveProcess()
        {
            var hwnd = NativeMethods.GetForegroundWindow();
            uint processId;
            NativeMethods.GetWindowThreadProcessId(hwnd, out processId);

            try
            {
                return Process.GetProcessById((int)processId);
            }
            catch (Win32Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return null;
        }
    }
}
