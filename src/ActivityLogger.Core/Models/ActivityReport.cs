﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.Core.Models
{
    public class ActivityReport
    {
        public ActivityReport()
        {
            SectionActivities = new Dictionary<string, ICollection<Activity>>();
            SectionHourGoals = new Dictionary<string, float>();
        }

        public DateTime Started => GetStarted();
        public Activity CurrentActivity => GetCurrentActivity();
        public ICollection<Activity> CurrentSection => SectionActivities[ActivityType]; 
        public IDictionary<string, ICollection<Activity>> SectionActivities { get; set; }
        public float CurrentHourGoal => SectionHourGoals[ActivityType];
        public IDictionary<string, float> SectionHourGoals { get; set; }

        public string ProcessName => ProcessReport?.Name;
        public string ProcessDescription => ProcessReport?.Description;
        public ProcessReport ProcessReport { get; set; }

        public double TotalDistance => MouseReport?.TotalDistance ?? default(double);
        public MouseReport MouseReport { get; set; }

        public int TotalClicks => MouseClickReport?.TotalClicks ?? default(int);
        public MouseClickReport MouseClickReport { get; set; }

        public int TotalKeyStrokes => KeyReport?.TotalKeyStrokes ?? default(int);
        public KeyReport KeyReport { get; set; }

        public int TimeSinceLastReport => TimeReport?.Seconds ?? default(int);
        public DateTime LastActive { get; set; } = DateTime.Now;
        public TimeReport TimeReport { get; set; }

        public bool UserIsActive { get; set; }
        public bool UserIsIdle { get; set; } = false;
        public string ActivityType { get; set; } = string.Empty;

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
            public int Clicks { get; set; }
            public int KeyStrokes { get; set; }
            public double Distance { get; set; }

            public TimeSpan Elapsed => TimeSpan.FromSeconds(Seconds);
            public int Percent => (DateTime.Now - Started).Seconds * 100 / Seconds;
        }

        private Activity GetCurrentActivity()
        {
            if (string.IsNullOrEmpty(ActivityType))
                return null;

            return SectionActivities[ActivityType]?.FirstOrDefault(x => x.ProcessName == ProcessName);
        }

        private DateTime GetStarted()
        {
            return SectionActivities[ActivityType].OrderBy(x => x.Started).Select(x => x.Started).First();
        }
        
        public int PercentOfWorkDay()
        {
            if (CurrentHourGoal.Equals((float)0.0))
                return 0;

            return (int) (TotalTime*100/CurrentHourGoal/60/60);
        }
        public int TotalTime => SectionActivities.Sum(activity => activity.Value.Sum(process => process.Seconds));

        public int TotalCurrentActivityTime => TotalActivityTime(ActivityType);
        public TimeSpan ElapsedCurrentActivityTime => ElapsedActivityTime(ActivityType);
        public string ElapsedCurrentActivityTimeString => ElapsedActivityTimeString(ActivityType);

        public int TotalActivityTime(string activity)
        {
            if (!SectionActivities.ContainsKey(activity))
                return 0;

            return SectionActivities[activity].Sum(x => x.Seconds);
        }

        public string ElapsedActivityTimeString(string activity)
            => ElapsedActivityTime(activity).ToString("g");

        public TimeSpan ElapsedActivityTime(string activity)
            => TimeSpan.FromSeconds(TotalActivityTime(activity));
    }
}
