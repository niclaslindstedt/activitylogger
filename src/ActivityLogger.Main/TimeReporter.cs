using System;
using System.Collections.Generic;
using ActivityLogger.Core.Constants;
using ActivityLogger.Core.Services;
using TimeReport = System.Tuple<string, ActivityLogger.Core.Constants.ActivityType, int>;

namespace ActivityLogger.Core
{
    public class TimeReporter : ActivityReporter<Tuple<string, ActivityType, int>>, ITimeReporter
    {
        public int GetWorkActivity(string processName)
            => GetSecondsForProcess(_workActivity, processName);

        public int GetNonWorkActivity(string processName)
            => GetSecondsForProcess(_nonWorkActivity, processName);

        public int GetIdleTime(string processName)
            => GetSecondsForProcess(_idleTime, processName);

        private readonly Dictionary<string, int> _workActivity = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _nonWorkActivity = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _idleTime = new Dictionary<string, int>();
        
        private readonly ITimeReceiver _timeReceiver;

        public TimeReporter(Settings settings, ITimeReceiver timeReceiver) : base(settings)
        {
            _timeReceiver = timeReceiver;
        }

        protected override void Act(TimeReport value)
        {
            var processName = value.Item1;
            var activityType = value.Item2;
            var seconds = value.Item3;

            switch (activityType)
            {
                case ActivityType.Work:
                    AddSecondsToProcess(_workActivity, processName, seconds);
                    break;

                case ActivityType.WorkRelated:
                    AddSecondsToProcess(_nonWorkActivity, processName, seconds);
                    break;

                case ActivityType.NonWorkRelated:
                    AddSecondsToProcess(_idleTime, processName, seconds);
                    break;

                case ActivityType.None:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            var activeSeconds = GetActiveSeconds(processName, activityType);
            var timeReport = new TimeReport(processName, activityType, activeSeconds);
            _timeReceiver.ReportTime(timeReport);
        }

        private static void AddSecondsToProcess(IDictionary<string, int> processActivityRecord, string processName, int seconds)
        {
            if (processActivityRecord.ContainsKey(processName))
                processActivityRecord[processName] += seconds;
            else
                processActivityRecord[processName] = seconds;
        }

        private int GetActiveSeconds(string processName, ActivityType activityType)
        {
            switch (activityType)
            {
                case ActivityType.Work:
                    return GetSecondsForProcess(_workActivity, processName);

                case ActivityType.WorkRelated:
                    return GetSecondsForProcess(_nonWorkActivity, processName);

                case ActivityType.NonWorkRelated:
                    return GetSecondsForProcess(_idleTime, processName);

                case ActivityType.None:
                    return 0;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static int GetSecondsForProcess(IDictionary<string, int> processActivityRecord, string processName)
        {
            return processActivityRecord.ContainsKey(processName) ? processActivityRecord[processName] : default(int);
        }
    }
}
