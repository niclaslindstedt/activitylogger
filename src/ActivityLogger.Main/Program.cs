using System;
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
        public static ITimeLogger TimeLogger;
        public static KeyReporter KeyReporter;
        public static MouseReporter MouseReporter;
        public static Settings Settings;

        private static string ActiveWorkProcess => ProcessService.GetActiveProcessFromList(Settings.WorkProcesses,
                        Settings.AllowedIdleSecondsForWork);
        private static string ActiveWorkRelatedProcess => ProcessService.GetActiveProcessFromList(Settings.WorkRelatedProcesses,
                        Settings.AllowedIdleSecondsForWorkRelated);

        static void Main(string[] args)
        {
            Settings = new Settings(new SettingsReader("ActivityLogger.ini"));
            ProcessService = new ProcessService();
            KeyReporter = new KeyReporter();
            KeyReporter.Subscribe(KeyLogger.Instance());
            MouseReporter = new MouseReporter();
            MouseReporter.Subscribe(MouseLogger.Instance());
            TimeLogger = new TimeLogger(ProcessService, KeyReporter, MouseReporter);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    
                    var activeSeconds = TimeLogger.ActiveSeconds.Sum(x => x.Value);
                    var procrastinationSeconds = TimeLogger.ProcrastinationSeconds.Sum(x => x.Value);
                    var idleSeconds = TimeLogger.IdleSeconds.Sum(x => x.Value);

                    Console.Clear();
                    Console.WriteLine($"Active for: {activeSeconds} seconds");
                    Console.WriteLine($"Procrastinating for: {procrastinationSeconds} seconds");
                    Console.WriteLine($"Idle for: {idleSeconds} seconds");
#if DEBUG
                    Console.WriteLine($"Current active process: {ProcessService.CurrentProcessName}");
#endif
                    Console.WriteLine($"Currently active work process: {ActiveWorkProcess ?? ActiveWorkRelatedProcess}");
                    Console.WriteLine($"Mouse activity: {MouseReporter.UserIsActive}");
                    Console.WriteLine($"Keyboard activity: {KeyReporter.UserIsActive}");

                    TimeLogger.LogTime();
                }
            });

            Application.Run();
        }
    }
}
