﻿using System;
using System.Drawing;
using System.Windows.Forms;
using AL.Core;

namespace AL.Gui
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var applicationWindow = new ActivityLoggerWindow();
            var reportCentral = new ReportCentral(applicationWindow, applicationWindow);
            
            reportCentral.StartReporterThread();

            Application.Run(applicationWindow);
        }
    }
}
