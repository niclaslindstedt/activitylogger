﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkLogger.Main.Constants;
using WorkLogger.Main.Services;

namespace WorkLogger.Main
{
    internal class Program
    {
        public static IProcessService ProcessService;
        public static KeyReporter KeyReporter;
        public static MouseReporter MouseReporter;

        public static int ActiveSeconds;
        private static readonly Dictionary<string, int> ActiveProcessesRecord = new Dictionary<string, int>();

        // These are directly work related processes
        public static List<string> WorkProcesses = new List<string>()
        {
            $"WorkLogger.Main .+? Microsoft Visual Studio", // Visual Studio
        };

        // These are indirectly work related processes, e.g. browsing for help in Chrome
        public static List<string> WorkRelatedProcesses = new List<string>()
        {
            "chrome.exe", // Google Chrome
            "slack.exe"
        };

        static void Main(string[] args)
        {
            ProcessService = new ProcessService();
            KeyReporter = new KeyReporter();
            KeyReporter.Subscribe(KeyLogger.Instance());
            MouseReporter = new MouseReporter();
            MouseReporter.Subscribe(MouseLogger.Instance());

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    var activeWorkProcess = ProcessService.GetActiveProcessFromList(WorkProcesses,
                        TimeConstraints.AllowedIdleSecondsForWork);
                    var activeWorkRelatedProcess = ProcessService.GetActiveProcessFromList(WorkRelatedProcesses,
                        TimeConstraints.AllowedIdleSecondsForWorkRelated);

                    Console.Clear();
                    Console.WriteLine($"Development environment active for: {ActiveSeconds} seconds");
                    Console.WriteLine($"Currently active work process: {activeWorkProcess ?? activeWorkRelatedProcess}");
                    Console.WriteLine($"Mouse activity: {MouseReporter.UserIsActive}");
                    Console.WriteLine($"Keyboard activity: {KeyReporter.UserIsActive}");

                    if (!KeyReporter.UserIsActive && !MouseReporter.UserIsActive)
                        continue;
                    
                    if (IsWorkProcessesActive() || IsWorkRelatedProcessesActive())
                    {
                        ActiveSeconds++;

                        var process = activeWorkProcess ?? activeWorkRelatedProcess;
                        if (process != null)
                        {
                            if (ActiveProcessesRecord.ContainsKey(process))
                                ActiveProcessesRecord[process]++;
                            else
                                ActiveProcessesRecord.Add(process, 1);
                        }
                    }
                }
            });

            Application.Run();

            File.WriteAllText("worklog.txt", $"Session: {DateTime.Now} active for {ActiveSeconds} seconds.");
        }

        private static bool IsWorkProcessesActive()
        {
            return WorkProcesses.Select(x => ProcessService.IsProcessActive(x, TimeConstraints.AllowedIdleSecondsForWork))
                .Any(x => x.Equals(true));
        }

        private static bool IsWorkRelatedProcessesActive()
        {
            return WorkRelatedProcesses.Select(x => ProcessService.IsProcessActive(x, TimeConstraints.AllowedIdleSecondsForWorkRelated))
                .Any(x => x.Equals(true));
        }
    }
}