using System;
using System.Windows.Forms;
using AL.Core;
using AL.Core.Utilities;

namespace AL.Gui
{
    internal static class Program
    {
        public static Settings Settings;
        
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings = new Settings(new SettingsReader("ActivityLogger.ini"));

            var applicationWindow = new ActivityLoggerWindow();
            var reportCentral = new ReportCentral(applicationWindow, Settings);

            reportCentral.StartReporterThread();

            Application.Run(applicationWindow);
        }
    }
}
