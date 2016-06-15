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
        private readonly IDictionary<string, IDictionary<string, TimeSpan>> _processTimes;

        public ActivityLoggerWindow()
        {
            InitializeComponent();

            _processTimes = new Dictionary<string, IDictionary<string, TimeSpan>>();
            
            LogMessage("Activity Logger initialized");
        }
        
        public void ReportActivity(ActivityReport activityReport)
        {
            progressBarActiveTime.SetPropertyThreadSafe("Value", activityReport.PercentOfWorkDay());

            if (activityReport.UserIsActive)
            {
                labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} ({activityReport.ActivityType})");

                labelActiveTime.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
                labelProgress.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
                labelActiveProcess.SetPropertyThreadSafe("ForeColor", Color.DarkGreen);
            }
            else
            {
                labelActiveProcessValue.SetPropertyThreadSafe("Text", $"{activityReport.ProcessDescription} (Idle)");

                labelActiveTime.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
                labelProgress.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
                labelActiveProcess.SetPropertyThreadSafe("ForeColor", Color.DarkRed);
            }
            labelActiveTimeValue.SetPropertyThreadSafe("Text", activityReport.ElapsedCurrentActivityTimeString);

            _processWindowContent = string.Empty;
            foreach (var section in activityReport.SectionActivities)
            {
                var sectionName = section.Key;
                var processes = section.Value;

                if (sectionName == string.Empty)
                    continue;

                var totalTime = activityReport.ElapsedActivityTimeString(sectionName);
                _processWindowContent += $"=== [{totalTime}] {sectionName.ToUpperInvariant()} ===" + Environment.NewLine;

                if (!_processTimes.ContainsKey(sectionName))
                    _processTimes.Add(sectionName, new Dictionary<string, TimeSpan>());

                foreach (var activity in processes)
                {
                    _processTimes[sectionName][activity.ProcessDescription] = activity.Elapsed;
                }

                UpdateProcess(sectionName);
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

        private void UpdateProcess(string section)
        {
            var orderedList = _processTimes[section].OrderByDescending(x => x.Value);
            for (var i = 0; i < orderedList.Count(); ++i)
            {
                var rank = (i + 1).ToString();
                var processDescription = orderedList.ElementAt(i).Key;
                var processTime = orderedList.ElementAt(i).Value.ToString("g");
                _processWindowContent += $"#{rank.PadRight(2)} [{processTime}] {processDescription}" + Environment.NewLine;
            }
        }
    }
}
