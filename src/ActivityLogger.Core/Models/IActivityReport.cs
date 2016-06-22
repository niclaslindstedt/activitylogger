using System;
using System.Collections.Generic;

namespace AL.Core.Models
{
    public interface IActivityReport
    {
        // Current state
        string ActivityType { get; set; }
        Activity CurrentActivity { get; }
        float CurrentActivityShare { get; }
        float CurrentHourGoal { get; }
        Section CurrentSection { get; set; }
        TimeSpan ElapsedCurrentActivityTime { get; }
        string ElapsedCurrentActivityTimeString { get; }
        int TotalCurrentActivityTime { get; }
        DateTime LastActive { get; set; }
        DateTime LastInputActivity { get; set; }
        TimeSpan TimeUntilIdle { get; set; }
        int TimeUntilIdlePercentage { get; set; }
        bool UserIsActive { get; set; }
        bool UserIsIdle { get; set; }

        // Reports
        KeyReport KeyReport { get; set; }
        MouseClickReport MouseClickReport { get; set; }
        MouseReport MouseReport { get; set; }
        ProcessReport ProcessReport { get; set; }
        TimeReport TimeReport { get; set; }
        IList<string> LogMessages { get; set; }

        // Processes
        string ProcessDescription { get; }
        string ProcessName { get; }

        // Sections
        IDictionary<string, Section> Sections { get; set; }

        // Time
        DateTime Started { get; }
        int TimeSinceLastReport { get; }
        int TotalTime { get; }
        TimeSpan ElapsedActivityTime(string activity);
        string ElapsedActivityTimeString(string activity);
        int PercentOfWorkDay();
        int TotalActivityTime(string activity);

        // Clicks
        int TotalClicks { get; }
        IDictionary<string, int> ClicksPerMinute { get; set; }

        // Distance
        double TotalDistance { get; }
        IDictionary<string, double> DistancePerMinute { get; set; }

        // KeyStrokes
        int TotalKeyStrokes { get; }
        IDictionary<string, int> KeyStrokesPerMinute { get; set; }
    }
}