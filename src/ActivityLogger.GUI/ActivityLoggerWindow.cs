using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AL.Core.Interfaces;
using AL.Core.Models;

namespace AL.Gui
{
    public partial class ActivityLoggerWindow : Form, IActivityReceiver, ILogReceiver
    {
        private string _logWindowContent;
        private string _processWindowContent;
        private readonly Dictionary<string, TimeSpan> _processTimes;

        public ActivityLoggerWindow()
        {
            InitializeComponent();

            _processTimes = new Dictionary<string, TimeSpan>();
            
            LogMessage("Activity Logger initialized");
        }
        
        public void ReportActivity(ActivityReport activityReport)
        {
            progressBarActiveTime.SetPropertyThreadSafe("Value", activityReport.PercentOfWorkDay);

            if (activityReport.UserIsWorking)
            {
                labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedProcessTimeString);
                labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Work)");

                labelActiveTime.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
                labelProgress.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
                labelActiveProcess.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
            }
            else
            {
                if (activityReport.UserIsActive)
                {
                    labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Leisure)");
                    labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedNonWorkTimeString);
                }
                else
                {
                    labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Idle)");
                    labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedIdleTimeString);
                }


                labelActiveProcessValue.SetPropertyThreadSafe("Text", activityReport.ProcessName);

                labelActiveTime.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
                labelProgress.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
                labelActiveProcess.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
            }


            foreach (var activity in activityReport.WorkActivities)
            {
                UpdateProcess(activity.ProcessDescription, activity.Elapsed);
            }
            textBoxProcesses.SetPropertyThreadSafe("Text", _processWindowContent);
        }

        public void Log(string logMessage)
        {
            LogMessage(logMessage);
        }

        private void LogMessage(string logMessage)
        {
            _logWindowContent += $"[{DateTime.Now.ToString("HH:mm:ss")}] {logMessage}" + Environment.NewLine;
            textBoxLog.SetPropertyThreadSafe("Text", _logWindowContent);
        }

        private void UpdateProcess(string processName, TimeSpan timeSpan)
        {
            _processTimes[processName] = timeSpan;

            _processWindowContent = string.Empty;
            var orderedList = _processTimes.OrderByDescending(x => x.Value);
            for (var i = 0; i < orderedList.Count(); ++i)
            {
                var rank = i + 1;
                var processDescription = orderedList.ElementAt(i).Key;
                var processTime = orderedList.ElementAt(i).Value.ToString("g");
                _processWindowContent += $"#{rank} [{processTime}] {processDescription}" + Environment.NewLine;
            }
        }
    }
}
