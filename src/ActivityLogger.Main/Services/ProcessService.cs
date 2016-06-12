using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ActivityLogger.Main.Services
{
    public class ProcessService : IProcessService
    {
        private readonly Dictionary<string, DateTime> _lastWorkActivity = new Dictionary<string, DateTime>();

        private Process _currentProcess;
        public string CurrentModuleName => _currentProcess.MainModule.ModuleName;
        public string CurrentWindowTitle => _currentProcess.MainWindowTitle;
        public string CurrentProcessName => _currentProcess.ProcessName;

        public string GetActiveProcessFromList(IEnumerable<string> matchStrings, int allowedIdleSeconds)
        {
            var matchString = matchStrings.FirstOrDefault(x => IsProcessActive(x, allowedIdleSeconds));
            if (matchString == null)
                return null;

            if (IsMatchToCurrentProcess(matchString))
                return CurrentModuleName;

            return null;
        }
        
        private bool IsMatchToCurrentProcess(string matchString)
        {
            var regex = new Regex(matchString);
            _currentProcess = GetActiveProcess();
            if (CurrentModuleName == matchString
                || regex.IsMatch(CurrentWindowTitle)
                || regex.IsMatch(CurrentProcessName))
                return true;

            return false;
        }

        public bool IsProcessActive(string matchString, int allowedIdleSeconds)
        {
            if (IsMatchToCurrentProcess(matchString))
            {
                _lastWorkActivity[matchString] = DateTime.Now;
                return true;
            }

            if (!_lastWorkActivity.ContainsKey(matchString))
                return false;
            
            return (DateTime.Now - _lastWorkActivity[matchString]) < TimeSpan.FromSeconds(allowedIdleSeconds);
        }

        private Process GetActiveProcess()
        {
            var hwnd = GetForegroundWindow();
            uint processId;
            GetWindowThreadProcessId(hwnd, out processId);

            try
            {
                _currentProcess = Process.GetProcessById((int) processId);
            }
            catch (Win32Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return _currentProcess;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
