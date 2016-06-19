using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AL.Core.Models;

namespace AL.Gui.Elements
{
    public class ProcessReportElement : IControlUpdater
    {
        public string Content { get; private set; }
        
        private readonly IDictionary<string, IDictionary<string, TimeSpan>> _processTimes;

        private IActivityReport _activityReport;

        private readonly Control _coupledControl;
        private readonly string _propertyName;

        public ProcessReportElement(Control coupledControl, string propertyName)
        {
            _coupledControl = coupledControl;
            _propertyName = propertyName;

            _processTimes = new Dictionary<string, IDictionary<string, TimeSpan>>();
        }

        public void Update(IActivityReport activityReport)
        {
            _activityReport = activityReport;
            
            Content = string.Empty;
            foreach (var section in activityReport.Sections.Values)
            {
                var sectionName = section.SectionName;
                var activities = section.Activities;

                if (sectionName == string.Empty)
                    continue;

                var totalTime = activityReport.ElapsedActivityTimeString(sectionName);
                Content += $"=== [{totalTime}] [{section.Clicks}] [{section.KeyStrokes}] {sectionName.ToUpperInvariant()} ===" + Environment.NewLine;

                if (!_processTimes.ContainsKey(sectionName))
                    _processTimes.Add(sectionName, new Dictionary<string, TimeSpan>());

                foreach (var activity in activities)
                {
                    _processTimes[sectionName][activity.ProcessDescription] = activity.Elapsed;
                }

                UpdateProcess(sectionName);
            }

            _coupledControl.SetPropertyThreadSafe(_propertyName, Content);
        }

        private void UpdateProcess(string section)
        {
            var orderedList = _processTimes[section].OrderByDescending(x => x.Value);
            var activities = _activityReport.Sections[section].Activities;
            for (var i = 0; i < orderedList.Count(); ++i)
            {
                var rank = (i + 1).ToString();
                var processDescription = orderedList.ElementAt(i).Key;
                var activity = activities.FirstOrDefault(x => x.ProcessDescription == processDescription);
                if (activity == null)
                    continue;

                var processTime = orderedList.ElementAt(i).Value.ToString("g");
                Content += $"#{rank.PadRight(2)} [{processTime}] [{activity.Clicks}] [{activity.KeyStrokes}] {processDescription}" + Environment.NewLine;
            }
        }
    }
}
