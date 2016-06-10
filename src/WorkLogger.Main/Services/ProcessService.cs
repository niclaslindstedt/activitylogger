using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WorkLogger.Main.Services
{
    public class ProcessService : IProcessService
    {
        private readonly Dictionary<string, DateTime> _lastWorkActivity = new Dictionary<string, DateTime>();

        private Process _currentProcess;
        private string _currentModuleName;
        private string _currentWindowTitle;

        public string GetActiveProcessFromList(IEnumerable<string> matchStrings, int allowedIdleSeconds)
        {
            var matchString = matchStrings.FirstOrDefault(x => IsProcessActive(x, allowedIdleSeconds));
            if (matchString == null)
                return null;

            if (IsMatchToCurrentProcess(matchString))
                return _currentModuleName;

            return null;
        }

        private bool IsMatchToCurrentProcess(string matchString)
        {
            var regex = new Regex(matchString);
            _currentProcess = GetActiveProcess();
            if (_currentModuleName == matchString || regex.IsMatch(_currentWindowTitle))
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

            _currentProcess = Process.GetProcessById((int) processId);
            _currentModuleName = _currentProcess.MainModule.ModuleName;
            _currentWindowTitle = _currentProcess.MainWindowTitle;

            return _currentProcess;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
