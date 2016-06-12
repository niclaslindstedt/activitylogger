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
        private readonly Dictionary<string, string> _processMatches = new Dictionary<string, string>();

        private static Process CurrentProcess => GetActiveProcess();
        public string CurrentProcessDescription => CurrentProcess?.MainModule.FileVersionInfo.FileDescription;
        public string CurrentModuleName => CurrentProcess?.MainModule.ModuleName;
        public string CurrentWindowTitle => CurrentProcess?.MainWindowTitle;
        public string CurrentProcessName => CurrentProcess?.ProcessName;

        public string GetActiveProcessFromList(IEnumerable<string> matchStrings, int allowedIdleSeconds)
        {
            var matchString = matchStrings.FirstOrDefault(x => IsProcessActive(x, allowedIdleSeconds));
            if (matchString == null)
                return null;

            return _processMatches[matchString];
        }
        
        private bool IsMatchToCurrentProcess(string matchString)
        {
            var regex = new Regex(matchString);
            if (CurrentModuleName == matchString
                || regex.IsMatch(CurrentWindowTitle)
                || regex.IsMatch(CurrentProcessName))
            {
                if (!_processMatches.ContainsKey(matchString))
                    _processMatches.Add(matchString, CurrentProcessDescription);
                return true;
            }

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

        private static Process GetActiveProcess()
        {
            var hwnd = GetForegroundWindow();
            uint processId;
            GetWindowThreadProcessId(hwnd, out processId);

            try
            {
                return Process.GetProcessById((int) processId);
            }
            catch (Win32Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return null;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}
