using System;
using System.Collections.Generic;
using System.Linq;
using ActivityLogger.Core.Constants;
using ActivityLogger.Core.Services;

namespace ActivityLogger.Core
{
    public class TimeLogger : ActivityLogger<Tuple<string, ActivityType, int>>, ITimeLogger
    {
        private readonly IProcessService _processService;
        private readonly List<IActivityReporter> _activityReporters;
        private DateTime _lastReportTime;

        public TimeLogger(IProcessService processService, params IActivityReporter[] activityReporters)
        {
            _processService = processService;
            _activityReporters = activityReporters.ToList();
            _lastReportTime = DateTime.Now;
        }

        public void LogTime(Settings settings)
        {
            var activityType = ActivityType.None;
            var activeProcess = _processService.GetActiveProcessFromList(settings.WorkProcesses,
                settings.AllowedIdleSecondsForWork)
                ?? _processService.GetActiveProcessFromList(settings.WorkRelatedProcesses,
                    settings.AllowedIdleSecondsForWorkRelated);
            if (activeProcess != null)
            {
                activityType = ActivityType.Work;
            }
            else
            {
                activeProcess = _processService.CurrentProcessDescription;
                if (activeProcess != null)
                {
                    activityType = _activityReporters.Any(x => x.UserIsActive)
                        ? ActivityType.WorkRelated
                        : ActivityType.NonWorkRelated;
                }
            }

            if (activityType != ActivityType.None)
                Observer.OnNext(new Tuple<string, ActivityType, int>(activeProcess, activityType, GetTimeDifference()));

            _lastReportTime = DateTime.Now;
        }

        private int GetTimeDifference()
        {
            return (DateTime.Now - _lastReportTime).Seconds;
        }
    }
}
