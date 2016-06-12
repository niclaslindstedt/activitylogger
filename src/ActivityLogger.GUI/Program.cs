using System;
using System.Windows.Forms;
using ActivityLogger.Core;

namespace ActivityLogger.GUI
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
            var reportCentral = new ReportCentral(applicationWindow);

            reportCentral.StartThread();

            Application.Run(applicationWindow);
        }
    }
}
