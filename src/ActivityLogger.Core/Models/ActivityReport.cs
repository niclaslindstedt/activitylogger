using System;
using System.Collections.Generic;
using System.Linq;
using AL.Core.Constants;

namespace AL.Core.Models
{
    public class ActivityReport
    {
        public ActivityReport()
        {
            WorkActivities = new List<Activity>();
            NonWorkActivities = new List<Activity>();
            IdlingActivities = new List<Activity>();
        }

        public DateTime Started => WorkActivities.OrderBy(x => x.Started).Select(x => x.Started).FirstOrDefault();
        public int PercentOfWorkDay => TotalWorkTime*100/8/60/60;
        public TimeSpan ElapsedWorkTime => TimeSpan.FromSeconds(TotalWorkTime);
        public TimeSpan ElapsedNonWorkTime => TimeSpan.FromSeconds(TotalNonWorkTime);
        public TimeSpan ElapsedIdleTime => TimeSpan.FromSeconds(TotalIdleTime);
        public TimeSpan ElapsedProcessTime => TimeSpan.FromSeconds(TotalProcessTime);
        public string ElapsedWorkTimeString => ElapsedWorkTime.ToString("g");
        public string ElapsedNonWorkTimeString => ElapsedNonWorkTime.ToString("g");
        public string ElapsedIdleTimeString => ElapsedIdleTime.ToString("g");
        public string ElapsedProcessTimeString => ElapsedProcessTime.ToString("g");
        public int TotalWorkTime => WorkActivities.Sum(x => x.Seconds);
        public int TotalNonWorkTime => NonWorkActivities.Sum(x => x.Seconds);
        public int TotalIdleTime => IdlingActivities.Sum(x => x.Seconds);
        public int TotalProcessTime => WorkActivities.FirstOrDefault(x => x.ProcessName == ProcessName)?.Seconds ?? default(int);

        public ICollection<Activity> WorkActivities { get; set; }
        public ICollection<Activity> NonWorkActivities { get; set; }
        public ICollection<Activity> IdlingActivities { get; set; }

        public string ProcessName => ProcessReport?.Name;
        public string ProcessDescription => ProcessReport?.Description;
        public ProcessReport ProcessReport { get; set; }

        public double MouseDistance => MouseReport?.Distance ?? default(double);
        public MouseReport MouseReport { get; set; }

        public int KeyStrokes => KeyReport?.KeyStrokes ?? default(int);
        public KeyReport KeyReport { get; set; }

        public int TimeSinceLastReport => TimeReport?.Seconds ?? default(int);
        public DateTime LastActive { get; set; } = DateTime.Now;
        public TimeReport TimeReport { get; set; }

        public bool UserIsWorking => ActivityType == ActivityType.Work || ActivityType == ActivityType.WorkRelated;
        public bool UserIsActive => ActivityType != ActivityType.None;
        public ActivityType ActivityType { get; set; }

        public class Activity
        {
            public Activity()
            {
                Started = DateTime.Now;
            }

            public DateTime Started { get; }
            public string ProcessName { get; set; }
            public string ProcessDescription { get; set; }
            public int Seconds { get; set; }

            public TimeSpan Elapsed => TimeSpan.FromSeconds(Seconds);
            public int Percent => (DateTime.Now - Started).Seconds * 100 / Seconds;
        }
    }
}
