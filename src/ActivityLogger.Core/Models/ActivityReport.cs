using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Core.Models
{
    public class ActivityReport : IActivityReport
    {
        public ActivityReport()
        {
            Sections = new Dictionary<string, Section>();
            ClicksPerMinute = new Dictionary<string, int>();
            DistancePerMinute = new Dictionary<string, double>();
            KeyStrokesPerMinute = new Dictionary<string, int>();
            LogMessages = new List<string>();
        }

        public IDictionary<string, Section> Sections { get; set; }

        public Section CurrentSection
        {
            get { return Sections.ContainsKey(ActivityType) ? Sections[ActivityType] : null; }
            set { Sections[ActivityType] = value; }
        }
        
        public DateTime Started => GetStarted();
        public Activity CurrentActivity => GetCurrentActivity();

        public float CurrentHourGoal => CurrentSection.HourGoal;

        public string ProcessName => ProcessReport?.Name;
        public string ProcessDescription => ProcessReport?.Description;
        public ProcessReport ProcessReport { get; set; }

        public double TotalDistance => MouseReport?.TotalDistance ?? default(double);
        public MouseReport MouseReport { get; set; }
        public IDictionary<string, double> DistancePerMinute { get; set; }

        public int TotalClicks => MouseClickReport?.TotalClicks ?? default(int);
        public MouseClickReport MouseClickReport { get; set; }
        public IDictionary<string, int> ClicksPerMinute { get; set; }

        public int TotalKeyStrokes => KeyReport?.TotalKeyStrokes ?? default(int);
        public KeyReport KeyReport { get; set; }
        public IDictionary<string, int> KeyStrokesPerMinute { get; set; }

        public int TimeSinceLastReport => TimeReport?.Seconds ?? default(int);
        public DateTime LastActive { get; set; } = DateTime.Now;
        public TimeReport TimeReport { get; set; }

        public bool UserIsActive { get; set; }
        public bool UserIsIdle { get; set; } = false;
        public string ActivityType { get; set; } = string.Empty;

        public IList<string> LogMessages { get; set; }

        private Activity GetCurrentActivity()
        {
            if (string.IsNullOrEmpty(ActivityType))
                return null;

            return Sections[ActivityType].Activities.FirstOrDefault(x => x.ProcessName == ProcessName);
        }

        private DateTime GetStarted()
        {
            return Sections[ActivityType].Activities.OrderBy(x => x.Started).Select(x => x.Started).First();
        }
        
        public int PercentOfWorkDay()
        {
            if (CurrentHourGoal.Equals((float)0.0))
                return 0;

            return (int) (TotalTime*100/CurrentHourGoal/60/60);
        }
        public int TotalTime => Sections.Sum(s => s.Value.Seconds);

        public int TotalCurrentActivityTime => TotalActivityTime(ActivityType);
        public TimeSpan ElapsedCurrentActivityTime => ElapsedActivityTime(ActivityType);
        public string ElapsedCurrentActivityTimeString => ElapsedActivityTimeString(ActivityType);

        public int TotalActivityTime(string activity)
        {
            if (activity == null || !Sections.ContainsKey(activity))
                return 0;

            return Sections[activity].Seconds;
        }

        public string ElapsedActivityTimeString(string activity)
            => ElapsedActivityTime(activity).ToString("g");

        public TimeSpan ElapsedActivityTime(string activity)
            => TimeSpan.FromSeconds(TotalActivityTime(activity));
    }
}
