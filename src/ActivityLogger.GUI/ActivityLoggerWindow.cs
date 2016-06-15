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

        private ActivityReport _activityReport;

        public ActivityLoggerWindow()
        {
            InitializeComponent();

            _processTimes = new Dictionary<string, IDictionary<string, TimeSpan>>();
            
            LogMessage("Activity Logger initialized");
        }
        
        public void ReportActivity(ActivityReport activityReport)
        {
            _activityReport = activityReport;

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
            foreach (var section in activityReport.Sections.Values)
            {
                var sectionName = section.SectionName;
                var activities = section.Activities;

                if (sectionName == string.Empty)
                    continue;

                var totalTime = activityReport.ElapsedActivityTimeString(sectionName);
                _processWindowContent += $"=== [{totalTime}] [{section.Clicks}] [{section.KeyStrokes}] [{sectionName.ToUpperInvariant()} ===" + Environment.NewLine;

                if (!_processTimes.ContainsKey(sectionName))
                    _processTimes.Add(sectionName, new Dictionary<string, TimeSpan>());

                foreach (var activity in activities)
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
            var activities = _activityReport.Sections[section].Activities;
            for (var i = 0; i < orderedList.Count(); ++i)
            {
                var rank = (i + 1).ToString();
                var processDescription = orderedList.ElementAt(i).Key;
                var activity = activities.First(x => x.ProcessDescription == processDescription);
                var processTime = orderedList.ElementAt(i).Value.ToString("g");
                _processWindowContent += $"#{rank.PadRight(2)} [{processTime}] [{activity.Clicks}] [{activity.KeyStrokes}] {processDescription}" + Environment.NewLine;
            }
        }

        private void ActivityLoggerWindow_Paint(object sender, PaintEventArgs e)
        {
            const int rectangleWidth = 554;
            const int rectangleHeight = 190;
            const int rectangleLeft = 15;
            const int rectangleTop = 15;
            const int rectangleBottom = rectangleTop + rectangleHeight;
            const int graphWidth = 3;
            const int axisPaddingX = 25;
            const int axisPaddingY = 15;

            var g = CreateGraphics();
            g.DrawRectangle(Pens.Gray, rectangleLeft - 1, rectangleTop - 1, rectangleWidth + 2, rectangleHeight + 2);
            g.FillRectangle(new SolidBrush(Color.White), rectangleLeft, rectangleTop, rectangleWidth, rectangleHeight);
            g.DrawLine(Pens.Black, rectangleLeft + axisPaddingX, rectangleTop + axisPaddingY, rectangleLeft + axisPaddingX, rectangleTop + rectangleHeight - axisPaddingY);
            g.DrawLine(Pens.Black, rectangleLeft + axisPaddingX, rectangleTop + rectangleHeight - axisPaddingY, rectangleLeft + rectangleWidth - axisPaddingX, rectangleTop + rectangleHeight - axisPaddingY);

            if (_activityReport?.KeyStrokesPerMinute == null || !_activityReport.KeyStrokesPerMinute.Any())
                return;

            var reportsToUse = _activityReport.KeyStrokesPerMinute
                .Select(x => x.Value)
                .Reverse()
                .Take(rectangleWidth / graphWidth)
                .Reverse()
                .ToArray();
            
            var maxKeyStrokes = reportsToUse.Max();

            labelAxisX.Text = maxKeyStrokes.ToString();

            for (var i = 0; i < reportsToUse.Length; ++i)
            {
                var keyStrokes = reportsToUse[i];

                Color color;
                if ((float)keyStrokes / maxKeyStrokes > 0.9)
                    color = Color.Green;
                else if ((float)keyStrokes/maxKeyStrokes > 0.6)
                    color = Color.YellowGreen;
                else if ((float)keyStrokes / maxKeyStrokes > 0.3)
                    color = Color.DarkOrange;
                else
                    color = Color.OrangeRed;

                var x = i * graphWidth + rectangleLeft + axisPaddingX + 1;
                var y = rectangleBottom - Math.Min((float)keyStrokes/maxKeyStrokes*rectangleBottom, rectangleBottom) + rectangleTop + axisPaddingY;
                var height = Math.Max(0, rectangleBottom - y) - axisPaddingY;

                g.FillRectangle(new SolidBrush(color), x, y, graphWidth, height);
            }
        }
    }
}
