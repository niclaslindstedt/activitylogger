using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActivityLogger.Main.Services;

namespace ActivityLogger.Main
{
    internal class Program
    {
        public static IProcessService ProcessService;
        public static KeyReporter KeyReporter;
        public static MouseReporter MouseReporter;
        public static Settings Settings;

        public static int ActiveSeconds;
        private static readonly Dictionary<string, int> ActiveProcessesRecord = new Dictionary<string, int>();
        
        static void Main(string[] args)
        {
            Settings = new Settings(new SettingsReader("ActivityLogger.ini"));
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

                    var activeWorkProcess = ProcessService.GetActiveProcessFromList(Settings.WorkProcesses,
                        Settings.AllowedIdleSecondsForWork);
                    var activeWorkRelatedProcess = ProcessService.GetActiveProcessFromList(Settings.WorkRelatedProcesses,
                        Settings.AllowedIdleSecondsForWorkRelated);

                    Console.Clear();
                    Console.WriteLine($"Development environment active for: {ActiveSeconds} seconds");
#if DEBUG
                    Console.WriteLine($"Current active process: {ProcessService.CurrentProcessName}");
#endif
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
            return Settings.WorkProcesses.Select(x => ProcessService.IsProcessActive(x, Settings.AllowedIdleSecondsForWork))
                .Any(x => x.Equals(true));
        }

        private static bool IsWorkRelatedProcessesActive()
        {
            return Settings.WorkRelatedProcesses.Select(x => ProcessService.IsProcessActive(x, Settings.AllowedIdleSecondsForWorkRelated))
                .Any(x => x.Equals(true));
        }
    }
}
